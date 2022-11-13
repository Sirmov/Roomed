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
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.Reservation;

    /// <summary>
    /// This class is a implementation of the <see cref="IReservationsService"/> interface.
    /// </summary>
    /// <inheritdoc cref="IReservationsService"/>
    public class ReservationsService : IReservationsService
    {
        private readonly IDeletableEntityRepository<Reservation, Guid> reservationsRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationsService"/> class.
        /// Uses constructor injection to resolve dependencies.
        /// </summary>
        /// <param name="reservationRepository">The <see cref="Reservation"/> database repository.</param>
        /// <param name="mapper">The global auto mapper.</param>
        public ReservationsService(IDeletableEntityRepository<Reservation, Guid> reservationRepository, IMapper mapper)
        {
            this.reservationsRepository = reservationRepository;
            this.mapper = mapper;
        }

        /// <inheritdoc />
        public Task CreateReservationAsync() => throw new NotImplementedException();

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
        public async Task<ICollection<ReservationDto>> GetAllAsync()
        {
            var reservations = await this.reservationsRepository
                .All()
                .ProjectTo<ReservationDto>(this.mapper.ConfigurationProvider)
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
        /// <exception cref="ArgumentNullException">Throws when id is null.</exception>
        /// <exception cref="FormatException">Throws when id is not in correct format.</exception>
        public async Task<ReservationDto> GetAsync(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var guid = Guid.Parse(id);
            var reservation = await this.reservationsRepository.FindAsync(guid);
            var reservationDto = mapper.Map<ReservationDto>(reservation);

            return reservationDto;
        }
    }
}
