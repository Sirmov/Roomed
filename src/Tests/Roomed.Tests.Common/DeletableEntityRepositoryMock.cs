namespace Roomed.Tests.Common
{
    using Microsoft.EntityFrameworkCore;
    using MockQueryable.Moq;
    using Moq;
    using Roomed.Data.Common.Models;
    using Roomed.Data.Common.Repositories;
    using Roomed.Data.Models;

    public static class DeletableEntityRepositoryMock<TEntity, TKey>
        where TEntity : BaseDeletableModel<TKey>
    {
        private static readonly ICollection<TEntity> entities = new List<TEntity>();

        public static IDeletableEntityRepository<TEntity, TKey> Instance
        {
            get
            {
                var mock = new Mock<IDeletableEntityRepository<TEntity, TKey>>();
                var entitiesMock = entities.BuildMock();

                mock.Setup(m => m.All(It.IsAny<bool>(), It.IsAny<bool>()))
                    .Returns((bool isReadonly, bool withDeleted) =>
                    {
                        IQueryable<TEntity> query = entitiesMock;

                        if (isReadonly)
                        {
                            query = query.AsNoTracking();
                        }

                        if (!withDeleted)
                        {
                            query = query.Where(e => !e.IsDeleted);
                        }

                        return query;
                    });

                mock.Setup(m => m.AddAsync(It.IsAny<TEntity>()).Result)
                    .Returns((TEntity entity) =>
                    {
                        entity.CreatedOn = DateTime.UtcNow;
                        entities.Add(entity);
                        return null;
                    });

                mock.Setup(m => m.FindAsync(It.IsAny<TKey>(), It.IsAny<bool>()).Result)
                    .Returns((TKey id, bool isReadonly) =>
                    {
                        ArgumentNullException.ThrowIfNull(id);

                        TEntity? entity = entities.FirstOrDefault(e => EqualityComparer<TKey>.Default.Equals(e.Id, id));

                        if (entity == null)
                        {
                            throw new InvalidOperationException("There is no entity found with this id!");
                        }

                        return entity;
                    });

                mock.Setup(m => m.Dispose()).Callback(() => entities.Clear());

                var repository = mock.Object;
                return repository;
            }
        }

        public static void Dispose()
        {
            entities.Clear();
        }
    }
}
