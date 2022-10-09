namespace Roomed.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Microsoft.EntityFrameworkCore;

    using Roomed.Data.Common.Models;
    using Roomed.Data.Common.Repositories;

    public class EfDeletableEntityRepository<TEntity, TKey> : EfRepository<TEntity, TKey>, IDeletableEntityRepository<TEntity, TKey>
        where TEntity : BaseDeletableModel<TKey>
    {
        public EfDeletableEntityRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public override IQueryable<TEntity> All() => base.All().Where(x => !x.IsDeleted);

        public override IQueryable<TEntity> All(Expression<Func<TEntity, bool>> search) => this.All().Where(search);

        public override IQueryable<TEntity> AllAsNoTracking() => base.AllAsNoTracking().Where(x => !x.IsDeleted);

        public override IQueryable<TEntity> AllAsNoTracking(Expression<Func<TEntity, bool>> search) => this.AllAsNoTracking().Where(search);

        public IQueryable<TEntity> AllWithDeleted() => base.All().IgnoreQueryFilters();

        public IQueryable<TEntity> AllWithDeleted(Expression<Func<TEntity, bool>> search) => this.AllWithDeleted().Where(search);

        public IQueryable<TEntity> AllWithDeletedAsNoTracking() => base.AllAsNoTracking().IgnoreQueryFilters();

        public IQueryable<TEntity> AllWithDeletedAsNoTracking(Expression<Func<TEntity, bool>> search) => this.AllWithDeletedAsNoTracking().Where(search);

        public void HardDelete(TEntity entity) => base.Delete(entity);

        public void Undelete(TEntity entity)
        {
            entity.IsDeleted = false;
            entity.DeletedOn = null;
            this.Update(entity);
        }

        public override void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            this.Update(entity);
        }
    }
}
