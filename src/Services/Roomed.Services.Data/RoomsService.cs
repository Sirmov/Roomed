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
    using Roomed.Services.Data.Dtos.Room;
    using Roomed.Services.Data.Dtos.RoomType;

    public class RoomsService : BaseService<Room, int>, IRoomsService
    {
        private readonly IDeletableEntityRepository<Room, int> roomsRepository;

        public RoomsService(
            IDeletableEntityRepository<Room, int> entityRepository,
            IMapper mapper)
            : base(entityRepository, mapper)
        {
            this.roomsRepository = entityRepository;
        }

        /// <inheritdoc/>
        public Task<ICollection<RoomDto>> GetAllRoomsAsync(RoomTypeDto? roomType = null, QueryOptions<RoomTypeDto>? queryOptions = null) => throw new NotImplementedException();

        /// <inheritdoc/>
        public Task<ICollection<RoomDto>> GetAllFreeRoomsAsync(DateOnly date, RoomTypeDto? roomType = null, QueryOptions<RoomTypeDto>? queryOptions = null) => throw new NotImplementedException();

        /// <inheritdoc/>
        public Task<ICollection<RoomDto>> GetAllFreeRoomsAsync(DateOnly startDate, DateOnly endDate, RoomTypeDto? roomType = null, QueryOptions<RoomTypeDto>? queryOptions = null) => throw new NotImplementedException();
    }
}
