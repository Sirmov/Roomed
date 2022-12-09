namespace Roomed.Services.Data.Contracts
{
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Dtos.RoomType;

    /// <summary>
    /// This interface is used to state and document the room types data service functionality.
    /// </summary>
    public interface IRoomTypesService
    {
        /// <summary>
        /// This method asynchronously returns a collection of all room types.
        /// </summary>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns a collection of <see cref="RoomTypeDto"/> objects.</returns>
        public Task<ICollection<RoomTypeDto>> GetAllAsync(QueryOptions<RoomTypeDto>? queryOptions = null);

        /// <summary>
        /// This method asynchronously returns the room type with the corresponding id.
        /// </summary>
        /// <param name="id">The id of the profile.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns the <see cref="RoomTypeDto"/> with the given id.</returns>
        public Task<RoomTypeDto> GetAsync(int id, QueryOptions<RoomTypeDto>? queryOptions = null);

        /// <summary>
        /// This method asynchronously checks if a room type exists.
        /// </summary>
        /// <param name="id">The id of the room type.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="bool"/>.</returns>
        public Task<bool> ExistsAsync(int id, QueryOptions<RoomTypeDto>? queryOptions = null);
    }
}
