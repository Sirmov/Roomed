namespace Roomed.Data.Common.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Roomed.Data.Common.Models;

    public interface IRepository<TEntity, TKey> : IDisposable
        where TEntity : BaseModel<TKey>
    {
        IQueryable<TEntity> All();

        IQueryable<TEntity> AllAsNoTracking();

        TEntity Find(TKey id);

        TEntity FindAsNoTracking(TKey id);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void DeleteRange(IEnumerable<TEntity> entities);

        Task<int> SaveChangesAsync();
    }
}
