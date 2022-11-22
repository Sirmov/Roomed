namespace Roomed.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Roomed.Data.Common.Models;
    using Roomed.Data.Common.Repositories;

    /// <summary>
    /// An implementation of the <see cref="IDeletableEntityRepository{TEntity, TKey}"/> interface for the Entity Framework Core ORM.
    /// </summary>
    /// <typeparam name="TEntity">This is the entity that represents the table in the database.</typeparam>
    /// <typeparam name="TKey">This is the type of the primary key of the entity.</typeparam>
    public class EfDeletableEntityRepository<TEntity, TKey> : EfRepository<TEntity, TKey>, IDeletableEntityRepository<TEntity, TKey>
        where TEntity : BaseDeletableModel<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EfDeletableEntityRepository{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="context">The ef core db context.</param>
        /// <exception cref="ArgumentNullException">Throws when the db context is null.</exception>
        public EfDeletableEntityRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        /// <inheritdoc/>
        public IQueryable<TEntity> All(bool isReadonly = false, bool withDeleted = false)
        {
            var query = base.All(isReadonly);

            if (!withDeleted)
            {
                query = query.Where(e => !e.IsDeleted);
            }

            return query;
        }

        /// <inheritdoc/>
        public IQueryable<TEntity> All(Expression<Func<TEntity, bool>> search, bool isReadonly = false, bool withDeleted = false)
        {
            var query = base.All(search, isReadonly);

            if (!withDeleted)
            {
                query = query.Where(e => !e.IsDeleted);
            }

            return query;
        }

        /// <summary>
        /// This method returns a expression tree with all entities.
        /// Depending on the <paramref name="isReadonly"/> parameter the entities will or will not be tracked.
        /// Only entities that are not marked as deleted will be returned.
        /// </summary>
        /// <param name="isReadonly">This flag decides whether the entities should be tracked or not.</param>
        /// <returns>Returns an unmaterialized query with all entities.</returns>
        public override IQueryable<TEntity> All(bool isReadonly = false) => base.All(isReadonly).Where(x => !x.IsDeleted);

        /// <summary>
        /// This method returns a expression tree with all entities.
        /// Depending on the <paramref name="isReadonly"/> parameter the entities will or will not be tracked.
        /// Filtration is done by using the <paramref name="search"/> delegate.
        /// Only entities that are not marked as deleted will be returned.
        /// </summary>
        /// <param name="search">This is a filtration delegate.</param>
        /// <param name="isReadonly">This flag decides whether the entities should be tracked or not.</param>
        /// <returns>Returns an unmaterialized filtered query with all entities.</returns>
        public override IQueryable<TEntity> All(Expression<Func<TEntity, bool>> search, bool isReadonly = false) => this.All(isReadonly).Where(search);

        /// <inheritdoc/>
        public void HardDelete(TEntity entity) => base.Delete(entity);

        /// <inheritdoc/>
        public void Undelete(TEntity entity)
        {
            entity.IsDeleted = false;
            entity.DeletedOn = null;
            this.Update(entity);
        }

        /// <summary>
        /// This method sets the entity's IsDelted flag to true and DeletedOn to <see cref="DateTime.UtcNow"/>.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public override void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            this.Update(entity);
        }
    }
}
