﻿// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IdentityDocumentsService.cs" company="Roomed">
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
    using Roomed.Data.Models;
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.IdentityDocument;

    /// <summary>
    /// This class is a implementation of the <see cref="IIdentityDocumentsService"/> interface.
    /// It's purpose is to abstract and encapsulate the business logic related to the <see cref="IdentityDocument"/> entity.
    /// </summary>
    public class IdentityDocumentsService : BaseService<IdentityDocument, Guid>, IIdentityDocumentsService
    {
        private readonly IDeletableEntityRepository<IdentityDocument, Guid> identityDocumentsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityDocumentsService"/> class.
        /// Uses constructor injection to resolve dependencies.
        /// </summary>
        /// <param name="identityDocumentsRepository">The <see cref="IdentityDocument"/> database repository.</param>
        /// <param name="mapper">The implementation of <see cref="IMapper"/>.</param>
        public IdentityDocumentsService(IDeletableEntityRepository<IdentityDocument, Guid> identityDocumentsRepository, IMapper mapper)
            : base(identityDocumentsRepository, mapper)
        {
            this.identityDocumentsRepository = identityDocumentsRepository;
        }

        /// <inheritdoc />
        public async Task<ICollection<IdentityDocumentDto>> GetAllAsync(QueryOptions<IdentityDocumentDto>? queryOptions = null)
        {
            return await base.GetAllAsync(queryOptions ?? new ());
        }

        /// <inheritdoc/>
        public async Task<IdentityDocumentDto> GetAsync(Guid id, QueryOptions<IdentityDocumentDto>? queryOptions = null)
        {
            return await base.GetAsync(id, queryOptions ?? new ());
        }

        /// <inheritdoc/>
        public async Task<Guid> CreateAsync(IdentityDocumentDto identityDocumentDto)
        {
            bool isValid = base.ValidateDto(identityDocumentDto);

            if (!isValid)
            {
                throw new ArgumentException("Identity document model state is not valid.", nameof(identityDocumentDto));
            }

            IdentityDocument model = this.mapper.Map<IdentityDocument>(identityDocumentDto);

            var result = await this.identityDocumentsRepository.AddAsync(model);
            await this.identityDocumentsRepository.SaveChangesAsync();

            return result.Entity.Id;
        }

        /// <inheritdoc/>
        public async Task EditAsync(Guid id, IdentityDocumentDto newIdentityDocument)
        {
            bool isValid = base.ValidateDto(newIdentityDocument);

            if (!isValid)
            {
                throw new ArgumentException("Identity document model state is not valid.", nameof(newIdentityDocument));
            }

            var oldIdentityDocument = await this.identityDocumentsRepository.FindAsync(id, false);

            if (id == newIdentityDocument.Id && newIdentityDocument.Id == oldIdentityDocument.Id)
            {
                oldIdentityDocument.OwnerId = newIdentityDocument.OwnerId;
                oldIdentityDocument.Type = newIdentityDocument.Type;
                oldIdentityDocument.NameInDocument = newIdentityDocument.NameInDocument;
                oldIdentityDocument.DocumentNumber = newIdentityDocument.DocumentNumber;
                oldIdentityDocument.PersonalNumber = newIdentityDocument.PersonalNumber;
                oldIdentityDocument.Country = newIdentityDocument.Country;
                oldIdentityDocument.Birthdate = newIdentityDocument.Birthdate;
                oldIdentityDocument.PlaceOfBirth = newIdentityDocument.PlaceOfBirth;
                oldIdentityDocument.Nationality = newIdentityDocument.Nationality;
                oldIdentityDocument.ValidFrom = newIdentityDocument.ValidFrom;
                oldIdentityDocument.ValidUntil = newIdentityDocument.ValidUntil;
                oldIdentityDocument.IssuedBy = newIdentityDocument.IssuedBy;
            }

            await this.identityDocumentsRepository.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(Guid id)
        {
            await this.identityDocumentsRepository.DeleteAsync(id);
            await this.identityDocumentsRepository.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> ExistsAsync(Guid id, QueryOptions<IdentityDocumentDto>? queryOptions = null)
        {
            var result = true;

            try
            {
                await this.identityDocumentsRepository.FindAsync(id);
            }
            catch (InvalidOperationException)
            {
                result = false;
            }

            return result;
        }
    }
}
