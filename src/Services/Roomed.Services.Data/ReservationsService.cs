namespace Roomed.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Roomed.Data.Models;
    using Roomed.Data.Models.Enums;
    using Roomed.Data.Repositories;
    using Roomed.Services.Data.Contracts;

    /// <summary>
    /// This class is a implementation of the <see cref="IReservationsService"/> interface.
    /// </summary>
    /// <inheritdoc cref="IReservationsService"/>
    public class ReservationsService : IReservationsService
    {
        private readonly EfDeletableEntityRepository<Reservation, Guid> reservationRepository;

        public ReservationsService(EfDeletableEntityRepository<Reservation, Guid> reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        /// <inheritdoc />
        public Task CreateReservationAsync() => throw new NotImplementedException();

        /// <inheritdoc />
        public async Task<ICollection<Reservation>> GetAllArrivingFromDateAsync(DateOnly date)
        {
            var reservations = await this.reservationRepository
                .All()
                .Include(r => r.ReservationDays)
                .Where(r => r.Status == ReservationStatus.Arriving && r.ReservationDays
                    .Any(rd => rd.Date == date))
                .ToListAsync();

            return reservations;
        }

        /// <inheritdoc />
        public async Task<ICollection<Reservation>> GetAllAsync()
        {
            var reservations = await this.reservationRepository.All().ToListAsync();

            return reservations;
        }

        /// <inheritdoc />
        public async Task<ICollection<Reservation>> GetAllDepartingFromDateAsync(DateOnly date)
        {
            var reservations = await this.reservationRepository
                .All()
                .Include(r => r.ReservationDays)
                .Where(r => r.Status == ReservationStatus.Departuring && r.ReservationDays
                    .Any(rd => rd.Date == date))
                .ToListAsync();

            return reservations;
        }

        /// <inheritdoc />
        public async Task<ICollection<Reservation>> GetAllInHouseFromDateAsync(DateOnly date)
        {
            var reservations = await this.reservationRepository
                .All()
                .Include(r => r.ReservationDays)
                .Where(r => r.Status == ReservationStatus.InHouse && r.ReservationDays
                    .Any(rd => rd.Date == date))
                .ToListAsync();

            return reservations;
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">Throws when id is null.</exception>
        /// <exception cref="FormatException">Throws when id is not in correct format.</exception>
        public async Task<Reservation> GetAsync(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            try
            {
                var guid = Guid.Parse(id);
                var reservation = await this.reservationRepository.FindAsync(guid);
                return reservation;
            }
            catch (FormatException fex)
            {
                throw fex;
            }
        }
    }
}
