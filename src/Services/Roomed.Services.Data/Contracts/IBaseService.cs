// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IBaseService.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Contracts
{
    using Roomed.Services.Data.Common;

    /// <summary>
    /// This interface is used to state and document the functionality that every data service should have.
    /// </summary>
    /// <typeparam name="TKey">The type of the primary key of the entity.</typeparam>
    public interface IBaseService<TKey>
    {
        /// <summary>
        /// This method asynchronously returns a collection of all entities of type <typeparamref name="TDto"/>.
        /// </summary>
        /// <typeparam name="TDto">The dto of the entity that should be returned.</typeparam>
        /// <param name="queryOptions">The options for the query.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> with a collection of all entities of type <typeparamref name="TDto"/>.</returns>
        public Task<ICollection<TDto>> GetAllAsync<TDto>(QueryOptions<TDto> queryOptions);

        /// <summary>
        /// This method asynchronously returns a single entity of type <typeparamref name="TDto"/>.
        /// </summary>
        /// <typeparam name="TDto">The dto of the entity that should be returned.</typeparam>
        /// <param name="id">The id of the <typeparamref name="TDto"/> entity.</param>
        /// <param name="queryOptions">The options for the query.</param>
        /// <returns>Returns a single <typeparamref name="TDto"/> object.</returns>
        public Task<TDto> GetAsync<TDto>(TKey id, QueryOptions<TDto> queryOptions);

        /// <summary>
        /// This method asynchronously creates a new entity in the database.
        /// </summary>
        /// <typeparam name="TDto">The type of the entity.</typeparam>
        /// <param name="dto">The entity to be created.</param>
        /// <returns>Returns the id of the newly created entity.</returns>
        public Task<TKey> CreateAsync<TDto>(TDto dto);

        /// <summary>
        /// This method asynchronously updates the entity with the given id with the values of the new one.
        /// </summary>
        /// <typeparam name="TDto">The type of the entity.</typeparam>
        /// <param name="id">The id of the entity to be updated.</param>
        /// <param name="dto">The new entity.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        public Task EditAsync<TDto>(TKey id, TDto dto);

        /// <summary>
        /// This method asynchronously deletes the entity with the provided id.
        /// </summary>
        /// <typeparam name="TDto">The type of the entity.</typeparam>
        /// <param name="id">The id of the entity to be deleted.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        public Task DeleteAsync<TDto>(TKey id);

        /// <summary>
        /// This method asynchronously check if a entity with a give id exists.
        /// </summary>
        /// <typeparam name="TDto">The type of the entity.</typeparam>
        /// <param name="id">The id of the entity.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="bool"/>.</returns>
        public Task<bool> ExistsAsync<TDto>(TKey id, QueryOptions<TDto>? queryOptions = null);
    }
}
