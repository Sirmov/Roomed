namespace Roomed.Data.Common.Repositories
{
    using System.Linq;

    using Roomed.Data.Common.Models;

    public interface IDeletableEntityRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseDeletableModel<TKey>
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllWithDeletedAsNoTracking();

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}
