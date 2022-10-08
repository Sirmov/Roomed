namespace Roomed.Data.Common.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
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
        IQueryable<TEntity> All();

        IQueryable<TEntity> All(Expression<Func<TEntity, bool>> search);

        IQueryable<TEntity> AllAsNoTracking();

        IQueryable<TEntity> AllAsNoTracking(Expression<Func<TEntity, bool>> search);

        TEntity Find(TKey id);

        Task<TEntity> FindAsync(TKey id);

        TEntity FindAsNoTracking(TKey id);

        Task<TEntity> FindAsNoTrackingAsync(TKey id);

        EntityEntry<TEntity> Add(TEntity entity);

        Task<EntityEntry<TEntity>> AddAsync(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        Task UpdateAsync(TKey id);

        void UpdateRange(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        Task DeleteAsync(TKey id);

        void DeleteRange(IEnumerable<TEntity> entities);

        EntityEntry<TEntity> Detach(TEntity entity);

        Task<int> SaveChangesAsync();
    }
}
