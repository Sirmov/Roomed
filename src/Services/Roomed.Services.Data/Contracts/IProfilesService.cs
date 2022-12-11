// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IProfilesService.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

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
        /// <returns>Returns a collection of <see cref="DetailedProfileDto"/> objects.</returns>
        public Task<ICollection<DetailedProfileDto>> GetAllAsync(QueryOptions<DetailedProfileDto>? queryOptions = null);

        /// <summary>
        /// This method asynchronously returns the profile with the corresponding id.
        /// </summary>
        /// <param name="id">The id of the profile.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns the <see cref="DetailedProfileDto"/> with the given id.</returns>
        public Task<DetailedProfileDto> GetAsync(Guid id, QueryOptions<DetailedProfileDto>? queryOptions = null);

        /// <summary>
        /// This method asynchronously checks if a guest profile exists.
        /// </summary>
        /// <param name="id">The id of the guest profile.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="bool"/>.</returns>
        public Task<bool> ExistsAsync(Guid id, QueryOptions<DetailedProfileDto>? queryOptions = null);

        /// <summary>
        /// This method asynchronously creates a guest profile with detailed data.
        /// </summary>
        /// <param name="profileDto">The profile to be created.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="Guid"/> - the id of the newly created entity.</returns>
        public Task<Guid> CreateDetailedAsync(DetailedProfileDto profileDto);

        /// <summary>
        /// This method asynchronously updates the profile with the given id with the values of the new profile.
        /// </summary>
        /// <param name="id">The id of the profile to be updated.</param>
        /// <param name="profileDto">The new profile.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        public Task EditAsync(Guid id, DetailedProfileDto profileDto);

        /// <summary>
        /// This method asynchronously deletes the profile with the provided id.
        /// </summary>
        /// <param name="id">The id of the profile to be deleted.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        public Task DeleteAsync(Guid id);
    }
}
