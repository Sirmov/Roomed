﻿namespace Roomed.Services.Data.Contracts
{
    using Roomed.Data.Models;
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Dtos.Reservation;

    /// <summary>
    /// This interface is used to state and document the reservations data service functionality.
    /// </summary>
    public interface IReservationsService
    {
        /// <summary>
        /// This method asynchronously returns a collection of all reservations.
        /// </summary>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns a collection of <see cref="Reservation"/> objects.</returns>
        public Task<ICollection<ReservationDto>> GetAllAsync(QueryOptions<ReservationDto>? queryOptions = null);

        /// <summary>
        /// This method asynchronously returns a single reservation.
        /// </summary>
        /// <param name="id">The id of the reservation.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns a single <see cref="Reservation"/> object.</returns>
        public Task<ReservationDto> GetAsync(string id, QueryOptions<ReservationDto>? queryOptions = null);

        /// <summary>
        /// This method asynchronously returns all arriving reservations on a given date.
        /// </summary>
        /// <param name="date">The date of the reservation.</param>
        /// <returns>Returns a collection <see cref="Reservation"/> objects.</returns>
        public Task<ICollection<ReservationDto>> GetAllArrivingFromDateAsync(DateOnly date);

        /// <summary>
        /// This method asynchronously returns all in house reservations on a given date.
        /// </summary>
        /// <param name="date">The date of the reservations.</param>
        /// <returns>Returns a collection <see cref="Reservation"/> objects.</returns>
        public Task<ICollection<ReservationDto>> GetAllInHouseFromDateAsync(DateOnly date);

        /// <summary>
        /// This method asynchronously returns all departing reservations on a given date.
        /// </summary>
        /// <param name="date">The date of the reservations.</param>
        /// <returns>Returns a collection <see cref="Reservation"/> objects.</returns>
        public Task<ICollection<ReservationDto>> GetAllDepartingFromDateAsync(DateOnly date);

        /// <summary>
        /// This method asynchronously create a <see cref="Reservation"/> entity in the database.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        public Task CreateReservationAsync();
    }
}
