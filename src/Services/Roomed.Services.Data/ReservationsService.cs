namespace Roomed.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Roomed.Data.Models;
    using Roomed.Services.Data.Contracts;

    /// <summary>
    /// This class is a implementation of the <see cref="IReservationsService"/> interface.
    /// </summary>
    /// <inheritdoc cref="IReservationsService"/>
    public class ReservationsService : IReservationsService
    {
        /// <inheritdoc />
        public Task CreateReservationAsync() => throw new NotImplementedException();

        /// <inheritdoc />
        public Task<ICollection<Reservation>> GetAllArrivingFromDateAsync(DateOnly date) => throw new NotImplementedException();

        /// <inheritdoc />
        public Task<ICollection<Reservation>> GetAllAsync() => throw new NotImplementedException();

        /// <inheritdoc />
        public Task<ICollection<Reservation>> GetAllDepartingFromDateAsync(DateOnly date) => throw new NotImplementedException();

        /// <inheritdoc />
        public Task<ICollection<Reservation>> GetAllInHouseFromDateAsync(DateOnly date) => throw new NotImplementedException();

        /// <inheritdoc />
        public Task<Reservation> GetAsync(string id) => throw new NotImplementedException();
    }
}
