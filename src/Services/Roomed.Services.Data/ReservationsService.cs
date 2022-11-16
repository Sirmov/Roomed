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

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationsService"/> class.
        /// Uses constructor injection to resolve dependencies.
        /// </summary>
        /// <param name="reservationRepository">The <see cref="Reservation"/> database repository.</param>
        /// <param name="mapper">The global auto mapper.</param>
        public ReservationsService(IDeletableEntityRepository<Reservation, Guid> reservationRepository, IMapper mapper)
            : base(reservationRepository, mapper)
        {
            this.reservationsRepository = reservationRepository;
        }

        /// <inheritdoc/>
        public async Task<ReservationDto> GetAsync(string id, QueryOptions<ReservationDto>? queryOptions = null)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var guid = Guid.Parse(id);

            return await base.GetAsync(guid, queryOptions ?? new ());
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
        public Task CreateReservationAsync() => throw new NotImplementedException();
    }
}
