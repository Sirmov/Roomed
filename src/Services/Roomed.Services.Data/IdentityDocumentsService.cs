namespace Roomed.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using Roomed.Data.Common.Repositories;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.IdentityDocument;

    public class IdentityDocumentsService : IIdentityDocumentsService
    {
        private readonly IDeletableEntityRepository<IdentityDocument, Guid> identityDocumentsRepository;
        private readonly IMapper mapper;

        public IdentityDocumentsService(IDeletableEntityRepository<IdentityDocument, Guid> identityDocumentsRepository, IMapper mapper)
        {
            this.identityDocumentsRepository = identityDocumentsRepository;
            this.mapper = mapper;
        }

        public async Task<ICollection<IdentityDocumentDto>> GetAllAsync()
        {
            var documents = await this.identityDocumentsRepository
                .All()
                .ProjectTo<IdentityDocumentDto>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            return documents;
        }
    }
}
