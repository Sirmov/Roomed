namespace Roomed.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Roomed.Data.Common.Models;
    using Roomed.Data.Common.Repositories;

    /// <summary>
    /// An implementation of the <see cref="IRepository{TEntity, TKey}"/> interface for the Entity Framework Core ORM.
    /// </summary>
    /// <typeparam name="TEntity">This is the entity that represents the table in the database.</typeparam>
    /// <typeparam name="TKey">This is the type of the primary key of the entity.</typeparam>
    public class EfRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseModel<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EfRepository{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="context">The ef core db context.</param>
        /// <exception cref="ArgumentNullException">Throws when the db context is null.</exception>
        public EfRepository(ApplicationDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.DbSet = this.Context.Set<TEntity>();
        }

        /// <summary>
        /// The entity's db set of the context.
        /// </summary>
        protected DbSet<TEntity> DbSet { get; set; }

        /// <summary>
        /// The application db context.
        /// </summary>
        protected ApplicationDbContext Context { get; set; }

        /// <inheritdoc/>
        public virtual IQueryable<TEntity> All(bool isReadonly = false)
        {
            if (isReadonly)
            {
                return this.DbSet.AsNoTracking();
            }

            return this.DbSet;
        }

        /// <inheritdoc/>
        public virtual IQueryable<TEntity> All(Expression<Func<TEntity, bool>> search, bool isReadonly = false)
        {

            var query = this.DbSet.Where(search);

            if (isReadonly)
            {
                return query.AsNoTracking();
            }

            return query;
        }

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">Throws when the entity can not be found.</exception>
        public virtual TEntity Find(TKey id, bool isReadonly = false)
        {
            TEntity? entity = this.DbSet.Find(id);

            if (entity == null)
            {
                throw new InvalidOperationException("There is no entity found with this id!");
            }

            if (isReadonly)
            {
                this.Context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">Throws when the entity can not be found.</exception>
        public virtual async Task<TEntity> FindAsync(TKey id, bool isReadonly = false)
        {
            TEntity? entity = await this.DbSet.FindAsync(id);

            if (entity == null)
            {
                throw new InvalidOperationException("There is no entity found with this id!");
            }

            if (isReadonly)
            {
                this.Context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        /// <inheritdoc/>
        public virtual EntityEntry<TEntity> Add(TEntity entity) => this.DbSet.Add(entity);

        /// <inheritdoc/>
        public virtual Task<EntityEntry<TEntity>> AddAsync(TEntity entity) => this.DbSet.AddAsync(entity).AsTask();

        /// <inheritdoc/>
        public virtual void AddRange(IEnumerable<TEntity> entities) => this.DbSet.AddRange(entities);

        /// <inheritdoc/>
        public virtual Task AddRangeAsync(IEnumerable<TEntity> entities) => this.DbSet.AddRangeAsync(entities);

        /// <inheritdoc/>
        public virtual void Update(TEntity entity)
        {
            var entry = this.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">Throws when the entity can not be found.</exception>
        public virtual async Task UpdateAsync(TKey id)
        {
            TEntity? entity = await this.FindAsync(id);

            if (entity == null)
            {
                throw new InvalidOperationException("There is no entity found with this id!");
            }

            this.Update(entity);
        }

        /// <inheritdoc/>
        public virtual void UpdateRange(IEnumerable<TEntity> entities) => this.DbSet.UpdateRange(entities);

        /// <inheritdoc/>
        public virtual void Delete(TEntity entity)
        {
            var entry = this.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Deleted;
        }

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">Throws when the entity can not be found.</exception>
        public virtual async Task DeleteAsync(TKey id)
        {
            TEntity? entity = await this.FindAsync(id);

            if (entity == null)
            {
                throw new InvalidOperationException("There is no entity found with this id!");
            }

            this.Delete(entity);
        }

        /// <inheritdoc/>
        public virtual void DeleteRange(IEnumerable<TEntity> entities) => this.DbSet.RemoveRange(entities);

        /// <inheritdoc/>
        public virtual EntityEntry<TEntity> Detach(TEntity entity)
        {
            var entry = this.Context.Entry(entity);

            entry.State = EntityState.Detached;
            return entry;
        }

        /// <inheritdoc/>
        public virtual Task<int> SaveChangesAsync() => this.Context.SaveChangesAsync();

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// This method checks if the repository is disposing and calls the <see cref="DbContext.Dispose"/> method.
        /// </summary>
        /// <param name="disposing">Flag showing whether the repository is disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Context?.Dispose();
            }
        }
    }
}
