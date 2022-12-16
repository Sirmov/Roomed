// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IReservationsService.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Contracts
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
        /// <returns>Returns a <see cref="Task{TResult}"/> with the collection of <see cref="ReservationDto"/> objects.</returns>
        public Task<ICollection<ReservationDto>> GetAllAsync(QueryOptions<ReservationDto>? queryOptions = null);

        /// <summary>
        /// This method asynchronously returns a single reservation.
        /// </summary>
        /// <param name="id">The id of the reservation.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> with a single <see cref="ReservationDto"/> object.</returns>
        public Task<ReservationDto> GetAsync(string id, QueryOptions<ReservationDto>? queryOptions = null);

        /// <summary>
        /// This method asynchronously returns all arriving reservations on a given date.
        /// </summary>
        /// <param name="date">The date of the reservation.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> with the collection of <see cref="ReservationDto"/> objects.</returns>
        public Task<ICollection<ReservationDto>> GetAllArrivingFromDateAsync(DateOnly date);

        /// <summary>
        /// This method asynchronously returns all in house reservations on a given date.
        /// </summary>
        /// <param name="date">The date of the reservations.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> with the collection of <see cref="ReservationDto"/> objects.</returns>
        public Task<ICollection<ReservationDto>> GetAllInHouseFromDateAsync(DateOnly date);

        /// <summary>
        /// This method asynchronously returns all departing reservations on a given date.
        /// </summary>
        /// <param name="date">The date of the reservations.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> with the collection of <see cref="ReservationDto"/> objects.</returns>
        public Task<ICollection<ReservationDto>> GetAllDepartingFromDateAsync(DateOnly date);

        ///// <summary>
        ///// This method asynchronously returns all arriving, in house or departing reservations on a given date.
        ///// </summary>
        ///// <param name="date">The date of the reservations.</param>
        ///// <param name="queryOptions">The query options.</param>
        ///// <returns>Returns a collection of <see cref="ReservationDto"/>
        ///// of all arriving, in house or departing reservations on a given date.</returns>
        // public Task<ICollection<ReservationDto>> GetAllOccupiedReservationsFromDateAsync(DateOnly date, QueryOptions<RoomTypeDto>? queryOptions = null);

        ///// <summary>
        ///// This method asynchronously returns all arriving, in house or departing reservations for a given period.
        ///// </summary>
        ///// <param name="startDate">The date of the start of the period.</param>
        ///// <param name="endDate">The date of the end of the period.</param>
        ///// <param name="queryOptions">The query options.</param>
        ///// <returns>Returns a collection of <see cref="ReservationDto"/>
        ///// of all arriving, in house or departing reservations for a given period.</returns>
        // public Task<ICollection<ReservationDto>> GetAllOccupiedReservationsFromDateAsync(DateOnly startDate, DateOnly endDate, QueryOptions<RoomTypeDto>? queryOptions = null);

        /// <summary>
        /// This method asynchronously create a <see cref="Reservation"/> entity in the database.
        /// </summary>
        /// <param name="reservationDto">The reservation to be created.</param>
        /// <param name="roomId">The id of the room to be occupied.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> with the id of the newly created reservation.</returns>
        public Task<Guid> CreateReservationAsync(ReservationDto reservationDto, int roomId);

        /// <summary>
        /// This method asynchronously determines whether a reservation with a given id exists.
        /// </summary>
        /// <param name="id">The id of the reservation.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="bool"/> indicating whether the reservation exists.</returns>
        public Task<bool> ExistsAsync(Guid id, QueryOptions<ReservationDto>? queryOptions = null);
    }
}
