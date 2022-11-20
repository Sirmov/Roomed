namespace Roomed.Services.Data.Contracts
{
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Dtos.Profile;

    /// <summary>
    /// This interface is used to state and document the profiles data service functionality.
    /// </summary>
    public interface IProfilesService
    {
        /// <summary>
        /// This method asynchronously returns a collection of all profiles.
        /// </summary>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns a collection of <see cref="ProfileDto"/> objects.</returns>
        public Task<ICollection<ProfileDto>> GetAllAsync(QueryOptions<ProfileDto>? queryOptions = null);

        /// <summary>
        /// This method asynchronously creates a guest profile with detailed data.
        /// </summary>
        /// <param name="profile">The profile to be created.</param>
        /// <returns>Returns a <see cref="Guid"/> - the id of the newly created entity.</returns>
        public Task<Guid> CreateDetailedAsync(DetailedProfileDto profile);
    }
}
