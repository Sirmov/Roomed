namespace Roomed.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using Roomed.Data.Common.Repositories;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.RoomType;

    public class RoomTypesService : BaseService<RoomType, int>, IRoomTypesService
    {
        private readonly IDeletableEntityRepository<RoomType, int> roomTypesRepository;

        public RoomTypesService(
            IDeletableEntityRepository<RoomType, int> entityRepository,
            IMapper mapper)
            : base(entityRepository, mapper)
        {
            this.roomTypesRepository = entityRepository;
        }

        /// <inheritdoc/>
        public async Task<ICollection<RoomTypeDto>> GetAllAsync(QueryOptions<RoomTypeDto>? queryOptions = null)
        {
            return await base.GetAllAsync(queryOptions ?? new ());
        }

        /// <inheritdoc/>
        public async Task<RoomTypeDto> GetAsync(int id, QueryOptions<RoomTypeDto>? queryOptions = null)
        {
            return await base.GetAsync(id, queryOptions ?? new ());
        }

        /// <inheritdoc/>
        public async Task<bool> ExistsAsync(int id, QueryOptions<RoomTypeDto>? queryOptions = null)
        {
            var result = true;

            try
            {
                await this.roomTypesRepository.FindAsync(id);
            }
            catch (InvalidOperationException iox)
            {
                result = false;
            }

            return result;
        }
    }
}
