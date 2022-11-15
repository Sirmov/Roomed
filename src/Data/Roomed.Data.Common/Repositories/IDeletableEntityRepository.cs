namespace Roomed.Data.Common.Repositories
{
    using System.Linq;
    using System.Linq.Expressions;

    using Roomed.Data.Common.Models;

    /// <summary>
    /// This is a repository interface that aims to abstract the direct use of the database context.
    /// This interfaced is preferred for deletable entities over the <see cref="IRepository{TEntity, TKey}"/> interface.
    /// </summary>
    /// <typeparam name="TEntity">This is the entity that represents the table in the database.</typeparam>
    /// <typeparam name="TKey">This is the type of the primary key of the entity.</typeparam>
    public interface IDeletableEntityRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseDeletableModel<TKey>
    {
        /// <summary>
        /// This method returns a expression tree with all entities.
        /// Depending on the <paramref name="isReadonly"/> parameter the entities will or will not be tracked.
        /// Depending on the <paramref name="withDeleted"/> parameter all or only the not deleted entities will be returned.
        /// </summary>
        /// <param name="isReadonly">This flag decides whether the entities should be tracked or not.</param>
        /// <param name="withDeleted">This flag decides whether all or only the not deleted entities should be returned.</param>
        /// <returns>Returns an unmaterialized query with the entities.</returns>
        IQueryable<TEntity> All(bool isReadonly, bool withDeleted);

        /// <summary>
        /// This method returns a expression tree with all entities.
        /// Depending on the <paramref name="isReadonly"/> parameter the entities will or will not be tracked.
        /// Depending on the <paramref name="withDeleted"/> parameter all or only the not deleted entities will be returned.
        /// Filtration is done by using the <paramref name="search"/> delegate.
        /// </summary>
        /// <param name="search">This is a filtration delegate.</param>
        /// <param name="isReadonly">This flag decides whether the entities should be tracked or not.</param>
        /// <param name="withDeleted">This flag decides whether all or only the not deleted entities should be returned.</param>
        /// <returns>Returns an unmaterialized filtered query with the entities.</returns>
        IQueryable<TEntity> All(Expression<Func<TEntity, bool>> search, bool isReadonly, bool withDeleted);

        /// <summary>
        /// This method deletes the entity from the database.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        void HardDelete(TEntity entity);

        /// <summary>
        /// This method makes the IsDeleted flag of the entity to false and DeletedOn to null.
        /// </summary>
        /// <param name="entity">The entity to be undeleted.</param>
        void Undelete(TEntity entity);
    }
}
