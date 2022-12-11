// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IRepository.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Common.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    using Roomed.Data.Common.Models;

    /// <summary>
    /// This is a repository interface that aims to abstract the direct use of the database context.
    /// </summary>
    /// <typeparam name="TEntity">This is the entity that represents the table in the database.</typeparam>
    /// <typeparam name="TKey">This is the type of the primary key of the entity.</typeparam>
    public interface IRepository<TEntity, TKey> : IDisposable
        where TEntity : BaseModel<TKey>
    {
        /// <summary>
        /// This method returns a expression tree with all entities.
        /// Depending on the <paramref name="isReadonly"/> parameter the entities will or will not be tracked.
        /// </summary>
        /// <param name="isReadonly">This flag decides whether the entities should be tracked or not.</param>
        /// <returns>Returns an unmaterialized query with all entities.</returns>
        IQueryable<TEntity> All(bool isReadonly = false);

        /// <summary>
        /// This method returns a expression tree with all entities.
        /// Depending on the <paramref name="isReadonly"/> parameter the entities will or will not be tracked.
        /// Filtration is done by using the <paramref name="search"/> delegate.
        /// </summary>
        /// <param name="search">This is a filtration delegate.</param>
        /// <param name="isReadonly">This flag decides whether the entities should be tracked or not.</param>
        /// <returns>Returns an unmaterialized filtered query with all entities.</returns>
        IQueryable<TEntity> All(Expression<Func<TEntity, bool>> search, bool isReadonly = false);

        /// <summary>
        /// This method finds an entity by its id.
        /// Depending on the <paramref name="isReadonly"/> parameter the entity will or will not be tracked.
        /// </summary>
        /// <param name="id">The id of the searched entity.</param>
        /// <param name="isReadonly">This flag decides whether the entities should be tracked or not.</param>
        /// <returns>Returns the found entity.</returns>
        TEntity Find(TKey id, bool isReadonly = false);

        /// <summary>
        /// This method asynchronously finds an entity by its id.
        /// Depending on the <paramref name="isReadonly"/> parameter the entity will or will not be tracked.
        /// </summary>
        /// <param name="id">The id of the searched entity.</param>
        /// <param name="isReadonly">This flag decides whether the entities should be tracked or not.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> with the found entity.</returns>
        Task<TEntity> FindAsync(TKey id, bool isReadonly = false);

        /// <summary>
        /// This method adds an entity to the database.
        /// </summary>
        /// <param name="entity">The entity that should be added.</param>
        /// <returns>Returns the <see cref="EntityEntry{TEntity}"/> of the entity.</returns>
        EntityEntry<TEntity> Add(TEntity entity);

        /// <summary>
        /// This method asynchronously adds an entity to the database.
        /// </summary>
        /// <param name="entity">The entity that should be added.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> with the <see cref="EntityEntry{TEntity}"/> of the entity.</returns>
        Task<EntityEntry<TEntity>> AddAsync(TEntity entity);

        /// <summary>
        /// This method adds a range of entities to the database.
        /// </summary>
        /// <param name="entities">The collection of entities to be added.</param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// This method asynchronously adds a range of entities to the database.
        /// </summary>
        /// <param name="entities">The collection of entities to be added.</param>
        /// <returns>Return a <see cref="Task"/>.</returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// This method attaches the entity if it is not tracked and changes its state to <see cref="EntityState.Modified"/>.
        /// </summary>
        /// <param name="entity">The entity that has been modified.</param>
        void Update(TEntity entity);

        /// <summary>
        /// This method asynchronously finds the entity, attaches it if it's not tracked and changes its state to <see cref="EntityState.Modified"/>.
        /// </summary>
        /// <param name="id">The id of the entity that has been modified.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        Task UpdateAsync(TKey id);

        /// <summary>
        /// This method attaches the entities if they are not tracked and changes their state to <see cref="EntityState.Modified"/>.
        /// </summary>
        /// <param name="entities">The collection of the modified entities.</param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// This method attaches the entity if it is not tracked and changes its state to <see cref="EntityState.Deleted"/>.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// This method asynchronously finds the deleted entity, attaches it if it's not tracked and changes its state to <see cref="EntityState.Deleted"/>.
        /// </summary>
        /// <param name="id">The id of the entity to be deleted.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        Task DeleteAsync(TKey id);

        /// <summary>
        /// This method attaches the entities if they are not tracked and changes their state to <see cref="EntityState.Deleted"/>.
        /// </summary>
        /// <param name="entities">The collection of the entities to be deleted.</param>
        void DeleteRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// This method detaches the entity's state.
        /// </summary>
        /// <param name="entity">The entity that should  be untracked.</param>
        /// <returns>Returns the <see cref="EntityEntry{TEntity}"/> of the entity.</returns>
        EntityEntry<TEntity> Detach(TEntity entity);

        /// <summary>
        /// This method calls the <see cref="DbContext.SaveChangesAsync(CancellationToken)"/> method of the db context.
        /// </summary>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="int"/>.</returns>
        Task<int> SaveChangesAsync();
    }
}
