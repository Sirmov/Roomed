// |-----------------------------------------------------------------------------------------------------|
// <copyright file="DeletableEntityRepositoryMock.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Tests.Common
{
    using Microsoft.EntityFrameworkCore;
    using MockQueryable.Moq;
    using Moq;

    using Roomed.Data.Common.Models;
    using Roomed.Data.Common.Repositories;

    /// <summary>
    /// This class is a mock of <see cref="IDeletableEntityRepository{TEntity, TKey}"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the primary key of the entity.</typeparam>
    public static class DeletableEntityRepositoryMock<TEntity, TKey>
        where TEntity : BaseDeletableModel<TKey>
    {
        private static readonly ICollection<TEntity> Entities = new List<TEntity>();

        /// <summary>
        /// Gets the <see cref="DeletableEntityRepositoryMock{TEntity, TKey}"/> instance of the mock.
        /// </summary>
        public static IDeletableEntityRepository<TEntity, TKey> Instance
        {
            get
            {
                var mock = new Mock<IDeletableEntityRepository<TEntity, TKey>>();
                var entitiesMock = Entities.BuildMock();

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
                        Entities.Add(entity);

                        return null!;
                    });

                mock.Setup(m => m.Add(It.IsAny<TEntity>()))
                    .Returns((TEntity entity) =>
                    {
                        entity.CreatedOn = DateTime.UtcNow;
                        Entities.Add(entity);

                        return null!;
                    });

                mock.Setup(m => m.FindAsync(It.IsAny<TKey>(), It.IsAny<bool>()).Result)
                    .Returns((TKey id, bool isReadonly) =>
                    {
                        ArgumentNullException.ThrowIfNull(id);

                        TEntity? entity = Entities.FirstOrDefault(e => EqualityComparer<TKey>.Default.Equals(e.Id, id));

                        if (entity == null)
                        {
                            throw new InvalidOperationException("There is no entity found with this id!");
                        }

                        return entity;
                    });

                mock.Setup(m => m.Find(It.IsAny<TKey>(), It.IsAny<bool>()))
                    .Returns((TKey id, bool isReadonly) =>
                    {
                        ArgumentNullException.ThrowIfNull(id);

                        TEntity? entity = Entities.FirstOrDefault(e => EqualityComparer<TKey>.Default.Equals(e.Id, id));

                        if (entity == null)
                        {
                            throw new InvalidOperationException("There is no entity found with this id!");
                        }

                        return entity;
                    });

                mock.Setup(m => m.DeleteAsync(It.IsAny<TKey>()))
                    .Returns((TKey id) =>
                    {
                        var entity = Entities.FirstOrDefault(e => EqualityComparer<TKey>.Default.Equals(e.Id, id));

                        if (entity != null)
                        {
                            entity.IsDeleted = true;
                        }

                        return Task.CompletedTask;
                    });

                mock.Setup(m => m.Dispose()).Callback(() => Entities.Clear());

                var repository = mock.Object;
                return repository;
            }
        }

        /// <summary>
        /// This method clears the internal entities collection.
        /// </summary>
        public static void Dispose()
        {
            Entities.Clear();
        }
    }
}
