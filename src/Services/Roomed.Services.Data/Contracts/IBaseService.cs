// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IBaseService.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Contracts
{
    using Roomed.Services.Data.Common;

    public interface IBaseService<TKey>
    {
        /// <summary>
        /// This method asynchronously returns a collection of all entities of type <typeparamref name="TDto"/>.
        /// </summary>
        /// <typeparam name="TDto">The dto of the entity that should be returned.</typeparam>
        /// <param name="queryOptions">The options for the query.</param>
        /// <returns>Returns a task with a collection of all entities of type <typeparamref name="TDto"/>.</returns>
        public Task<ICollection<TDto>> GetAllAsync<TDto>(QueryOptions<TDto> queryOptions);

        /// <summary>
        /// This method asynchronously returns a single entity of type <typeparamref name="TDto"/>.
        /// </summary>
        /// <typeparam name="TDto">The dto of the entity that should be returned.</typeparam>
        /// <param name="id">The id of the <typeparamref name="TDto"/> entity.</param>
        /// <param name="queryOptions">The options for the query.</param>
        /// <returns>Returns a single <typeparamref name="TDto"/> object.</returns>
        public Task<TDto> GetAsync<TDto>(TKey id, QueryOptions<TDto> queryOptions);
    }
}
