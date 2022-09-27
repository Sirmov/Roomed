namespace HospitalityManagmentSystem.Data.Common.Repositories
{
    using System.Linq;

    using HospitalityManagmentSystem.Data.Common.Models;

    public interface IDeletableEntityRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseDeletableModel<TKey>
        where TKey : struct
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllWithDeletedAsNoTracking();

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}
