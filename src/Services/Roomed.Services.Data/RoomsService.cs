// |-----------------------------------------------------------------------------------------------------|
// <copyright file="RoomsService.cs" company="Roomed">
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
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using Roomed.Data.Common.Repositories;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.Room;
    using Roomed.Services.Data.Dtos.RoomType;

    /// <summary>
    /// This class is a implementation of the <see cref="RoomsService"/> interface.
    /// It's purpose is to abstract and encapsulate the business logic related to the <see cref="Room"/> entity.
    /// </summary>
    public class RoomsService : BaseService<Room, int>, IRoomsService
    {
        private readonly IDeletableEntityRepository<Room, int> roomsRepository;
        private readonly IReservationDaysService reservationDaysService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomsService"/> class.
        /// Uses constructor injection to resolve dependencies.
        /// </summary>
        /// <param name="entityRepository">The implementation of <see cref="IDeletableEntityRepository{TEntity, TKey}"/>.</param>
        /// <param name="reservationDaysService">The implementation of <see cref="IReservationDaysService"/>.</param>
        /// <param name="mapper">The implementation of <see cref="IMapper"/>.</param>
        public RoomsService(
            IDeletableEntityRepository<Room, int> entityRepository,
            IReservationDaysService reservationDaysService,
            IMapper mapper)
            : base(entityRepository, mapper)
        {
            this.roomsRepository = entityRepository;
            this.reservationDaysService = reservationDaysService;
        }

        /// <inheritdoc/>
        public async Task<bool> ExistsAsync(int id, QueryOptions<RoomDto>? queryOptions = null)
        {
            var result = true;

            try
            {
                await this.roomsRepository.FindAsync(id);
            }
            catch (InvalidOperationException)
            {
                result = false;
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<ICollection<RoomDto>> GetAllAsync(RoomTypeDto? roomType = null, QueryOptions<RoomDto>? queryOptions = null)
        {
            var rooms = await base.GetAllAsync(queryOptions ?? new ());

            if (roomType != null)
            {
                rooms = rooms.Where(r => r.Type.Name == roomType.Name).ToList();
            }

            return rooms;
        }

        /// <inheritdoc/>
        public async Task<ICollection<RoomDto>> GetAllFreeRoomsAsync(DateOnly date, RoomTypeDto? roomType = null, QueryOptions<RoomDto>? queryOptions = null)
        {
            var reservationDays = await this.reservationDaysService.GetAllForDateAsync(date);

            if (roomType != null)
            {
                reservationDays = reservationDays
                    .Where(rd => rd.Room.Type.Name == roomType.Name)
                    .ToList();
            }

            var rooms = await this.GetAllAsync(roomType);

            var freeRooms = rooms
                .Where(r => !reservationDays.Any(rd => rd.Room.Id == r.Id))
                .ToList();

            return freeRooms;
        }

        /// <inheritdoc/>
        public async Task<ICollection<RoomDto>> GetAllFreeRoomsAsync(DateOnly startDate, DateOnly endDate, RoomTypeDto? roomType = null, QueryOptions<RoomDto>? queryOptions = null)
        {
            var reservationDays = await this.reservationDaysService.GetAllForPeriodAsync(startDate, endDate);

            if (roomType != null)
            {
                reservationDays = reservationDays
                    .Where(rd => rd.Room.Type.Name == roomType.Name)
                    .ToList();
            }

            var rooms = await this.GetAllAsync(roomType);

            var freeRooms = rooms
                .Where(r => !reservationDays.Any(rd => rd.Room.Id == r.Id))
                .ToList();

            return freeRooms;
        }
    }
}
