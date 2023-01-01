// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationsService.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using Roomed.Data.Common.Repositories;
    using Roomed.Data.Models;
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.Reservation;

    /// <summary>
    /// This class is a implementation of the <see cref="IReservationsService"/> interface.
    /// </summary>
    /// <inheritdoc cref="IReservationsService"/>
    public class ReservationsService : BaseService<Reservation, Guid>, IReservationsService
    {
        private readonly IDeletableEntityRepository<Reservation, Guid> reservationsRepository;
        private readonly IRoomsService roomsService;
        private readonly IReservationDaysService reservationDaysService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationsService"/> class.
        /// Uses constructor injection to resolve dependencies.
        /// </summary>
        /// <param name="reservationRepository">The <see cref="Reservation"/> database repository.</param>
        /// <param name="roomsService">The implementation of <see cref="IRoomsService"/>.</param>
        /// <param name="reservationDaysService">The implementation of <see cref="IReservationDaysService"/>.</param>
        /// <param name="mapper">The implementation of <see cref="IMapper"/>.</param>
        public ReservationsService(
            IDeletableEntityRepository<Reservation, Guid> reservationRepository,
            IRoomsService roomsService,
            IReservationDaysService reservationDaysService,
            IMapper mapper)
            : base(reservationRepository, mapper)
        {
            this.reservationsRepository = reservationRepository;
            this.roomsService = roomsService;
            this.reservationDaysService = reservationDaysService;
        }

        /// <inheritdoc/>
        public async Task<ReservationDto> GetAsync(Guid id, QueryOptions<ReservationDto>? queryOptions = null)
        {
            var reservation = await this.reservationsRepository
                .All()
                .Include(r => r.ReservationHolder)
                .FirstAsync(r => r.Id == id);

            var dto = this.mapper.Map<ReservationDto>(reservation);

            return dto;
        }

        /// <inheritdoc/>
        public async Task<ICollection<ReservationDto>> GetAllAsync(QueryOptions<ReservationDto>? queryOptions = null)
        {
            return await base.GetAllAsync(queryOptions ?? new ());
        }

        /// <inheritdoc />
        public async Task<ICollection<ReservationDto>> GetAllArrivingFromDateAsync(DateOnly date)
        {
            var reservations = await this.reservationsRepository
                .All()
                .Include(r => r.ReservationDays)
                .ProjectTo<ReservationDto>(this.mapper.ConfigurationProvider)
                .Where(r => r.Status == ReservationStatus.Arriving && r.ReservationDays
                    .Any(rd => rd.Date == date))
                .ToListAsync();

            return reservations;
        }

        /// <inheritdoc />
        public async Task<ICollection<ReservationDto>> GetAllDepartingFromDateAsync(DateOnly date)
        {
            var reservations = await this.reservationsRepository
                .All()
                .Include(r => r.ReservationDays)
                .ProjectTo<ReservationDto>(this.mapper.ConfigurationProvider)
                .Where(r => r.Status == ReservationStatus.Departuring && r.ReservationDays
                    .Any(rd => rd.Date == date))
                .ToListAsync();

            return reservations;
        }

        /// <inheritdoc />
        public async Task<ICollection<ReservationDto>> GetAllInHouseFromDateAsync(DateOnly date)
        {
            var reservations = await this.reservationsRepository
                .All()
                .Include(r => r.ReservationDays)
                .ProjectTo<ReservationDto>(this.mapper.ConfigurationProvider)
                .Where(r => r.Status == ReservationStatus.InHouse && r.ReservationDays
                    .Any(rd => rd.Date == date))
                .ToListAsync();

            return reservations;
        }

        /// <inheritdoc />
        public async Task<Guid> CreateReservationAsync(ReservationDto reservationDto, int roomId)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            if (reservationDto.ArrivalDate > today)
            {
                reservationDto.Status = ReservationStatus.Expected;
            }
            else if (reservationDto.ArrivalDate == today)
            {
                reservationDto.Status = ReservationStatus.Arriving;
            }
            else
            {
                throw new InvalidOperationException("Cannot make a reservation for the past.");
            }

            bool isValid = base.ValidateDto(reservationDto);

            if (!isValid)
            {
                throw new ArgumentException("Reservation model state is not valid.", nameof(reservationDto));
            }

            Reservation model = this.mapper.Map<Reservation>(reservationDto);

            var result = await this.reservationsRepository.AddAsync(model);
            await this.reservationsRepository.SaveChangesAsync();

            await this.reservationDaysService.CreateForReservationAsync(result?.Entity ?? model, roomId);

            return result?.Entity?.Id ?? Guid.Empty;
        }

        /// <inheritdoc/>
        public async Task<bool> ExistsAsync(Guid id, QueryOptions<ReservationDto>? queryOptions = null)
        {
            var result = true;

            try
            {
                await this.reservationsRepository.FindAsync(id);
            }
            catch (InvalidOperationException)
            {
                result = false;
            }

            return result;
        }
    }
}
