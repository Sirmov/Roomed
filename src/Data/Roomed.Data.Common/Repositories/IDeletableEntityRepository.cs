namespace Roomed.Data.Common.Repositories
{
    using System.Linq;
    using System.Linq.Expressions;
    using Roomed.Data.Common.Models;

    public interface IDeletableEntityRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseDeletableModel<TKey>
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllWithDeleted(Expression<Func<TEntity, bool>> search);

        IQueryable<TEntity> AllWithDeletedAsNoTracking();

        IQueryable<TEntity> AllWithDeletedAsNoTracking(Expression<Func<TEntity, bool>> search);

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}
