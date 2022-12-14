// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ProfilesService.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using Roomed.Data.Common.Repositories;
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.Profile;

    using Profile = Roomed.Data.Models.Profile;

    /// <summary>
    /// This class is a implementation of the <see cref="IProfilesService"/> interface.
    /// It's purpose is to abstract and encapsulate the business logic related to the <see cref="Profile"/> entity.
    /// </summary>
    public class ProfilesService : BaseService<Profile, Guid>, IProfilesService
    {
        private readonly IDeletableEntityRepository<Profile, Guid> profilesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilesService"/> class.
        /// Uses constructor injection to resolve dependencies.
        /// </summary>
        /// <param name="profilesRepository">The <see cref="Profile"/> database repository.</param>
        /// <param name="mapper">The implementation of <see cref="IMapper"/>.</param>
        public ProfilesService(IDeletableEntityRepository<Profile, Guid> profilesRepository, IMapper mapper)
            : base(profilesRepository, mapper)
        {
            this.profilesRepository = profilesRepository;
        }

        /// <inheritdoc/>
        public async Task<ICollection<DetailedProfileDto>> GetAllAsync(QueryOptions<DetailedProfileDto>? queryOptions = null)
        {
            return await base.GetAllAsync(queryOptions ?? new ());
        }

        /// <inheritdoc/>
        public async Task<Guid> CreateDetailedAsync(DetailedProfileDto profileDto)
        {
            bool isValid = base.ValidateDto(profileDto);

            if (!isValid)
            {
                throw new ArgumentException("Profile model state is not valid.", nameof(profileDto));
            }

            Profile model = this.mapper.Map<Profile>(profileDto);

            var result = await this.profilesRepository.AddAsync(model);
            await this.profilesRepository.SaveChangesAsync();

            return result?.Entity?.Id ?? Guid.Empty;
        }

        /// <inheritdoc/>
        public async Task<DetailedProfileDto> GetAsync(Guid id, QueryOptions<DetailedProfileDto>? queryOptions = null)
        {
            return await base.GetAsync(id, queryOptions ?? new ());
        }

        /// <inheritdoc/>
        public async Task EditAsync(Guid id, DetailedProfileDto newProfile)
        {
            if (!await this.ExistsAsync(id))
            {
                throw new InvalidOperationException("No profile with this id can be found.");
            }

            bool isValid = base.ValidateDto(newProfile);

            if (!isValid)
            {
                throw new ArgumentException("Profile model state is not valid.", nameof(newProfile));
            }

            var oldProfile = await this.profilesRepository.FindAsync(id, false);

            if (id == newProfile.Id && newProfile.Id == oldProfile.Id)
            {
                oldProfile.FirstName = newProfile.FirstName;
                oldProfile.MiddleName = newProfile.MiddleName;
                oldProfile.LastName = newProfile.LastName;
                oldProfile.Birthdate = newProfile.Birthdate;
                oldProfile.Nationality = newProfile.Nationality;
                oldProfile.NationalityCode = newProfile.NationalityCode;
                oldProfile.Gender = newProfile.Gender;
                oldProfile.Address = newProfile.Address;
            }

            await this.profilesRepository.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(Guid id)
        {
            if (!await this.ExistsAsync(id))
            {
                throw new InvalidOperationException("No profile with this id can be found.");
            }

            await this.profilesRepository.DeleteAsync(id);
            await this.profilesRepository.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> ExistsAsync(Guid id, QueryOptions<DetailedProfileDto>? queryOptions = null)
        {
            var result = true;

            try
            {
                await this.profilesRepository.FindAsync(id);
            }
            catch (InvalidOperationException)
            {
                result = false;
            }

            return result;
        }
    }
}
