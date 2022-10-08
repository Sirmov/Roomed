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

    public class EfRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseModel<TKey>
    {
        public EfRepository(ApplicationDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.DbSet = this.Context.Set<TEntity>();
        }

        protected DbSet<TEntity> DbSet { get; set; }

        protected ApplicationDbContext Context { get; set; }

        public virtual IQueryable<TEntity> All() => this.DbSet;

        public virtual IQueryable<TEntity> All(Expression<Func<TEntity, bool>> search) => this.DbSet.Where(search);

        public virtual IQueryable<TEntity> AllAsNoTracking() => this.DbSet.AsNoTracking();

        public virtual IQueryable<TEntity> AllAsNoTracking(Expression<Func<TEntity, bool>> search) => this.DbSet.Where(search).AsNoTracking();

        public virtual TEntity? Find(TKey id) => this.DbSet.Find(id);

        public virtual Task<TEntity?> FindAsync(TKey id) => this.DbSet.FindAsync(id).AsTask();

        public virtual TEntity FindAsNoTracking(TKey id)
        {
            TEntity? entity = this.Find(id);

            if (entity == null)
            {
                throw new InvalidOperationException("There is no entity found with this id!");
            }

            this.Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual async Task<TEntity> FindAsNoTrackingAsync(TKey id)
        {
            TEntity? entity = await this.FindAsync(id);

            if (entity == null)
            {
                throw new InvalidOperationException("There is no entity found with this id!");
            }

            this.Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual EntityEntry<TEntity> Add(TEntity entity) => this.DbSet.Add(entity);

        public virtual Task<EntityEntry<TEntity>> AddAsync(TEntity entity) => this.DbSet.AddAsync(entity).AsTask();

        public virtual void AddRange(IEnumerable<TEntity> entities) => this.DbSet.AddRange(entities);

        public virtual Task AddRangeAsync(IEnumerable<TEntity> entities) => this.DbSet.AddRangeAsync(entities);

        public virtual void Update(TEntity entity)
        {
            var entry = this.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual async Task UpdateAsync(TKey id)
        {
            TEntity? entity = await this.FindAsync(id);

            if (entity == null)
            {
                throw new InvalidOperationException("There is no entity found with this id!");
            }

            this.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities) => this.DbSet.UpdateRange(entities);

        public virtual void Delete(TEntity entity)
        {
            var entry = this.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Deleted;
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            TEntity? entity = await this.FindAsync(id);

            if (entity == null)
            {
                throw new InvalidOperationException("There is no entity found with this id!");
            }

            this.Delete(entity);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities) => this.DbSet.RemoveRange(entities);

        public virtual EntityEntry<TEntity> Detach(TEntity entity)
        {
            var entry = this.Context.Entry(entity);

            entry.State = EntityState.Detached;
            return entry;
        }

        public virtual Task<int> SaveChangesAsync() => this.Context.SaveChangesAsync();

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Context?.Dispose();
            }
        }
    }
}
