// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IReservationDaysService.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Contracts
{
    using Roomed.Data.Models;
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Dtos.ReservationDay;

    /// <summary>
    /// This interface is used to state and document the reservation days data service functionality.
    /// </summary>
    public interface IReservationDaysService
    {
        /// <summary>
        /// This method returns all reservation days asynchronously.
        /// </summary>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> with a collection of all <see cref="ReservationDayDto"/>.</returns>
        public Task<ICollection<ReservationDayDto>> GetAllAsync(QueryOptions<ReservationDayDto>? queryOptions = null);

        /// <summary>
        /// This method returns the reservation day with the given id asynchronously.
        /// </summary>
        /// <param name="id">The id of the reservation day.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="ReservationDayDto"/>.</returns>
        public Task<ReservationDayDto> GetAsync(Guid id, QueryOptions<ReservationDayDto>? queryOptions = null);

        /// <summary>
        /// This method return all reservation days in a given data.
        /// </summary>
        /// <param name="date">The date of the reservation days.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> with a collection of all <see cref="ReservationDayDto"/> on a given date.</returns>
        public Task<ICollection<ReservationDayDto>> GetAllForDate(DateOnly date, QueryOptions<ReservationDayDto>? queryOptions = null);

        /// <summary>
        /// This method return all reservation days in a given period.
        /// </summary>
        /// <param name="startDate">The start of the period.</param>
        /// <param name="endDate">The end of the period.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> with a collection of all <see cref="ReservationDayDto"/> on a given period.</returns>
        public Task<ICollection<ReservationDayDto>> GetAllForPeriod(DateOnly startDate, DateOnly endDate, QueryOptions<ReservationDayDto>? queryOptions = null);

        /// <summary>
        /// This method creates all reservation days for a newly created reservation.
        /// </summary>
        /// <param name="reservation">The newly created reservation.</param>
        /// <param name="roomId">The id of the room to be occupied.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        public Task CreateForReservationAsync(Reservation reservation, int roomId);
    }
}
