// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationDaysService.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using Roomed.Data.Common.Repositories;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.ReservationDay;

    /// <summary>
    /// This class is a implementation of the <see cref="IReservationDaysService"/> interface.
    /// It's purpose is to abstract and encapsulate the business logic related to the <see cref="ReservationDay"/> entity.
    /// </summary>
    public class ReservationDaysService : BaseService<ReservationDay, Guid>, IReservationDaysService
    {
        private readonly IDeletableEntityRepository<ReservationDay, Guid> reservationDaysRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationDaysService"/> class.
        /// </summary>
        /// <param name="entityRepository">The implementation of <see cref="IDeletableEntityRepository{TEntity, TKey}"/>.</param>
        /// <param name="mapper">The implementation of <see cref="IMapper"/>.</param>
        public ReservationDaysService(
            IDeletableEntityRepository<ReservationDay, Guid> entityRepository,
            IMapper mapper)
            : base(entityRepository, mapper)
        {
            this.reservationDaysRepository = entityRepository;
        }

        /// <inheritdoc/>
        public async Task<ICollection<ReservationDayDto>> GetAllAsync(QueryOptions<ReservationDayDto>? queryOptions = null)
        {
            return await base.GetAllAsync(queryOptions ?? new ());
        }

        /// <inheritdoc/>
        public async Task<ReservationDayDto> GetAsync(Guid id, QueryOptions<ReservationDayDto>? queryOptions = null)
        {
            return await base.GetAsync(id, queryOptions ?? new ());
        }

        /// <inheritdoc/>
        public async Task<ICollection<ReservationDayDto>> GetAllForDateAsync(DateOnly date, QueryOptions<ReservationDayDto>? queryOptions = null)
        {
            var dtos = await this.reservationDaysRepository
                .All(queryOptions?.IsReadOnly ?? false, queryOptions?.WithDeleted ?? false)
                .Include(rd => rd.Room)
                .ThenInclude(r => r.Type)
                .Where(rd => rd.Date == date)
                .ProjectTo<ReservationDayDto>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            return dtos;
        }

        /// <inheritdoc/>
        public async Task<ICollection<ReservationDayDto>> GetAllForPeriodAsync(DateOnly startDate, DateOnly endDate, QueryOptions<ReservationDayDto>? queryOptions = null)
        {
            var dtos = await this.reservationDaysRepository
                .All(queryOptions?.IsReadOnly ?? false, queryOptions?.WithDeleted ?? false)
                .Include(rd => rd.Room)
                .ThenInclude(r => r.Type)
                .Where(rd => rd.Date >= startDate && rd.Date <= endDate)
                .ProjectTo<ReservationDayDto>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            return dtos;
        }

        /// <inheritdoc/>
        public async Task CreateForReservationAsync(Reservation reservation, int roomId)
        {
            bool reservationDaysExist = await this.reservationDaysRepository
                .All()
                .AnyAsync(rd => rd.ReservationId == reservation.Id);

            if (reservationDaysExist)
            {
                throw new InvalidOperationException("There are reservation days for this reservation already.");
            }

            int reservationLength = (reservation.DepartureDate.DayNumber - reservation.ArrivalDate.DayNumber) + 1;
            var reservationDays = new List<ReservationDay>();

            for (int i = 0; i < reservationLength; i++)
            {
                var currentDate = new DateOnly(
                        reservation.ArrivalDate.Year,
                        reservation.ArrivalDate.Month,
                        reservation.ArrivalDate.Day);
                currentDate = currentDate.AddDays(i);

                var reservationDay = new ReservationDay
                {
                    ReservationId = reservation.Id,
                    RoomId = roomId,
                    Date = currentDate,
                };

                reservationDays.Add(reservationDay);
            }

            await this.reservationDaysRepository.AddRangeAsync(reservationDays);
            await this.reservationDaysRepository.SaveChangesAsync();
        }
    }
}
