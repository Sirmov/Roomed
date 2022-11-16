namespace Roomed.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using Roomed.Data.Common.Repositories;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.IdentityDocument;

    /// <summary>
    /// This class is a implementation of the <see cref="IIdentityDocumentsService"/> interface.
    /// </summary>
    public class IdentityDocumentsService : BaseService<IdentityDocument, Guid>, IIdentityDocumentsService
    {
        private readonly IDeletableEntityRepository<IdentityDocument, Guid> identityDocumentsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityDocumentsService"/> class.
        /// Uses constructor injection to resolve dependencies.
        /// </summary>
        /// <param name="identityDocumentsRepository">The <see cref="IdentityDocument"/> database repository.</param>
        /// <param name="mapper">The global auto mapper.</param>
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
    }
}
