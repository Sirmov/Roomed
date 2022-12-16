// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IRoomsService.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Contracts
{
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Dtos.Room;
    using Roomed.Services.Data.Dtos.RoomType;

    /// <summary>
    /// This interface is used to state and document the rooms data service functionality.
    /// </summary>
    public interface IRoomsService
    {
        /// <summary>
        /// This method asynchronously returns all room of the provided type.
        /// If no type is specified all types should be returned.
        /// </summary>
        /// <param name="roomType">The type of the searched rooms.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns a collection of all rooms with the given type.</returns>
        public Task<ICollection<RoomDto>> GetAllAsync(RoomTypeDto? roomType = null, QueryOptions<RoomDto>? queryOptions = null);

        /// <summary>
        /// This method asynchronously returns all free rooms of a given type for a given day.
        /// If no type is specified all types should be returned.
        /// </summary>
        /// <param name="date">The day of occupation.</param>
        /// <param name="roomType">The type of the searched rooms.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns a collection of all free rooms of the given type.</returns>
        public Task<ICollection<RoomDto>> GetAllFreeRoomsAsync(DateOnly date, RoomTypeDto? roomType = null, QueryOptions<RoomDto>? queryOptions = null);

        /// <summary>
        /// This method asynchronously returns all free rooms of a given type for a given period.
        /// If no type is specified all types should be returned.
        /// </summary>
        /// <param name="startDate">The date of the start of the period.</param>
        /// <param name="endDate">The date of the end of the period.</param>
        /// <param name="roomType">The type of the searched rooms.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns a collection of all free rooms of the given type for the given period.</returns>
        public Task<ICollection<RoomDto>> GetAllFreeRoomsAsync(DateOnly startDate, DateOnly endDate, RoomTypeDto? roomType = null, QueryOptions<RoomDto>? queryOptions = null);

        /// <summary>
        /// This method asynchronously determines whether a room with a given id exists.
        /// </summary>
        /// <param name="id">The id of the room.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="bool"/> indicating whether the room exists.</returns>
        public Task<bool> ExistsAsync(int id, QueryOptions<RoomDto>? queryOptions = null);
    }
}
