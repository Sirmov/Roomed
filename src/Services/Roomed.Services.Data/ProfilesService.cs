namespace Roomed.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using Roomed.Data.Common.Repositories;
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.Profile;
    using Roomed.Services.Data.Dtos.Reservation;

    using Profile = Roomed.Data.Models.Profile;

    /// <summary>
    /// This class is a implementation of the <see cref="IProfilesService"/> interface.
    /// </summary>
    public class ProfilesService : BaseService<Profile, Guid>, IProfilesService
    {
        private readonly IDeletableEntityRepository<Profile, Guid> profilesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilesService"/> class.
        /// Uses constructor injection to resolve dependencies.
        /// </summary>
        /// <param name="profilesRepository">The <see cref="Profile"/> database repository.</param>
        /// <param name="mapper">The global auto mapper.</param>
        public ProfilesService(IDeletableEntityRepository<Profile, Guid> profilesRepository, IMapper mapper)
            : base(profilesRepository, mapper)
        {
            this.profilesRepository = profilesRepository;
        }

        /// <inheritdoc />
        public async Task<ICollection<ProfileDto>> GetAllAsync(QueryOptions<ProfileDto>? queryOptions = null)
        {
            return await base.GetAllAsync(queryOptions ?? new ());
        }
    }
}
