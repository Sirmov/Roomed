// <copyright file="EfDeletableRepositoryTests.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// </copyright>

namespace Roomed.Data.Tests
{
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using Roomed.Common.Constants;
    using Roomed.Data.Models;
    using Roomed.Data.Repositories;
    using Roomed.Tests.Common;

    /// <summary>
    /// This class contains all unit tests for <see cref="EfDeletableEntityRepository{TEntity, TKey}"/>.
    /// </summary>
    [TestFixture]
    public class EfDeletableRepositoryTests
    {
        private readonly ICollection<ReservationNote> reservationNotes = new List<ReservationNote>
        {
            new ReservationNote()
            {
                Id = Guid.Parse("2bfff802-5afb-4bbb-96b3-27c98161ff00"),
                Body = "Reservation note #1",
                IsDeleted = false,
            },
            new ReservationNote()
            {
                Id = Guid.Parse("bb2f7b6c-d8d9-4e2c-b14b-3bd98e18ad86"),
                Body = "Reservation note #2",
                IsDeleted = true,
            },
            new ReservationNote()
            {
                Id = Guid.Parse("08bd1b0d-15fd-4d2e-9f59-979d09da1133"),
                Body = "Reservation note #3",
                IsDeleted = false,
            },
        }.AsReadOnly();

        private ApplicationDbContext dbContext;

        /// <summary>
        /// This method is called before every test.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        [SetUp]
        public async Task Setup()
        {
            this.dbContext = await DbContextMock.InitializeDbContextAsync();
            await this.dbContext.AddRangeAsync(this.reservationNotes);
            await this.dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// This method is called after every test.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        [TearDown]
        public async Task TearDown()
        {
            await DbContextMock.DisposeAsync();
        }

        /// <summary>
        /// This test checks whether <see cref="EfDeletableEntityRepository{TEntity, TKey}.All(bool, bool)"/>
        /// returns all entities including deleted.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // All(bool isReadonly = false, bool withDeleted = false)
        [Test]
        public async Task AllShouldReturnAllEntitiesIncludingDeleted()
        {
            // Arrange
            var repository = new EfDeletableEntityRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var entities = await repository.All(false, true).ToListAsync();

            // Assert
            Assert.That(entities.Count, Is.EqualTo(3));
            Assert.That(entities, Has.All.Matches<ReservationNote>(rn => rn.IsDeleted || !rn.IsDeleted));
        }

        /// <summary>
        /// This test checks whether <see cref="EfDeletableEntityRepository{TEntity, TKey}.All(bool)"/>
        /// returns all not deleted entities.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        [Test]
        public async Task AllShouldReturnAllNotDeletedEntities()
        {
            // Arrange
            var repository = new EfDeletableEntityRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var entities = await repository.All().ToListAsync();

            // Assert
            Assert.That(
                entities.Count,
                Is.EqualTo(this.dbContext.ReservationNotes.Count(x => !x.IsDeleted)),
                "Entities count is not correct.");

            Assert.That(entities.Any(rn => rn.IsDeleted), Is.False, "Only not deleted entities should be returned");
        }

        /// <summary>
        /// This test checks whether <see cref="EfDeletableEntityRepository{TEntity, TKey}.All(System.Linq.Expressions.Expression{Func{TEntity, bool}}, bool)"/>
        /// returns all not deleted entities satisfying the condition.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // All(Expression<Func<TEntity, bool>> search, bool isReadonly = false)
        [Test]
        public async Task AllShouldReturnAllFilteredNotDeletedEntities()
        {
            // Arrange
            var repository = new EfDeletableEntityRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var entities = await repository.All(rn => rn.Body.Contains("Reservation note")).ToListAsync();

            // Assert
            Assert.That(
                entities.Count,
                Is.EqualTo(this.dbContext.ReservationNotes.Count(x => !x.IsDeleted)),
                "Entities count is not correct.");

            Assert.That(entities.Any(rn => rn.IsDeleted), Is.False, "Only not deleted entities should be returned");
        }

        /// <summary>
        /// This test checks whether <see cref="EfDeletableEntityRepository{TEntity, TKey}.All(System.Linq.Expressions.Expression{Func{TEntity, bool}}, bool, bool)"/>
        /// returns all not deleted entities satisfying the condition without tracking.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // All(Expression<Func<TEntity, bool>> search, bool isReadonly = false, bool withDeleted = false)
        [Test]
        public async Task AllShouldReturnAllFilteredNotDeletedEntitiesWithoutTracking()
        {
            // Arrange
            var repository = new EfDeletableEntityRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var entities = await repository.All(rn => rn.Body.Contains("Reservation note"), true).ToListAsync();

            // Assert
            Assert.That(
                entities.Count,
                Is.EqualTo(this.dbContext.ReservationNotes.Count(x => !x.IsDeleted)),
                "Entities count is not correct.");

            Assert.That(entities.Any(rn => rn.IsDeleted), Is.False, "Only not deleted entities should be returned");
            Assert.That(entities.All(rn => this.dbContext.Entry(rn).State == EntityState.Detached), Is.True);
        }

        /// <summary>
        /// This test checks whether <see cref="EfDeletableEntityRepository{TEntity, TKey}.HardDelete(TEntity)"/>
        /// deletes the entity from the database.
        /// </summary>
        /// <param name="id">The id of the entity to be deleted.</param>
        /// <param name="body">The body of the entity to be deleted.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        /// <exception cref="InvalidOperationException">Throws when no entity with <paramref name="id"/> can be found.</exception>
        // HardDelete(TEntity entity)
        [Test]
        [TestCase("d00528ac-df34-4c63-a5c8-b3dd031f17bb", "I will be gone forever.")]
        public async Task HardDeleteShouldDeleteEntityFromDatabase(string id, string body)
        {
            // Arrange
            var repository = new EfDeletableEntityRepository<ReservationNote, Guid>(this.dbContext);
            Guid guid = Guid.Parse(id);
            await this.dbContext.ReservationNotes.AddAsync(new ReservationNote { Id = guid, Body = body });
            await this.dbContext.SaveChangesAsync();

            // Act
            var entity = await this.dbContext
                .ReservationNotes
                .FindAsync(guid)
                ?? throw new InvalidOperationException(string.Format(ErrorMessagesConstants.EntityNotFound, "entity"));

            repository.HardDelete(entity);

            // Assert
            Assert.That(this.dbContext.Entry(entity).State, Is.EqualTo(EntityState.Deleted));
            await repository.SaveChangesAsync();

            var entities = await this.dbContext.ReservationNotes.ToListAsync();
            Assert.That(entities.Any(rn => rn.Id == guid), Is.False);
            Assert.That(entities.Any(rn => rn.Body == body), Is.False);
        }

        /// <summary>
        /// This test checks whether <see cref="EfDeletableEntityRepository{TEntity, TKey}.Delete(TEntity)"/>
        /// marks the entity as deleted and saves the date of the act.
        /// </summary>
        /// <param name="id">The id of the entity to be deleted.</param>
        /// <param name="body">The body of the entity to be deleted.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        /// <exception cref="InvalidOperationException">Throws when no entity with <paramref name="id"/> can be found.</exception>
        // Delete(TEntity entity)
        [Test]
        [TestCase("68fb55cb-1593-4961-b43a-20a315228bb4", "I will go missing.")]
        public async Task DeleteShouldSetIsDeletedFlagUpdateEntityAndSetDeletedOn(string id, string body)
        {
            // Arrange
            var repository = new EfDeletableEntityRepository<ReservationNote, Guid>(this.dbContext);
            Guid guid = Guid.Parse(id);
            await this.dbContext.ReservationNotes.AddAsync(new ReservationNote { Id = guid, Body = body });
            await this.dbContext.SaveChangesAsync();

            // Act
            var entity = await this.dbContext
                .ReservationNotes
                .FindAsync(guid)
                ?? throw new InvalidOperationException(string.Format(ErrorMessagesConstants.EntityNotFound, "entity"));
            repository.Delete(entity);

            // Assert
            Assert.That(this.dbContext.Entry(entity).State, Is.EqualTo(EntityState.Modified));
            await repository.SaveChangesAsync();

            entity = await this.dbContext
                .ReservationNotes
                .FindAsync(guid)
                ?? throw new InvalidOperationException(string.Format(ErrorMessagesConstants.EntityNotFound, "entity"));
            Assert.That(entity.IsDeleted, Is.True);
            Assert.That(entity.DeletedOn, Is.Not.Null);
        }

        /// <summary>
        /// This test checks whether <see cref="EfDeletableEntityRepository{TEntity, TKey}.Undelete(TEntity)"/>
        /// marks the entity as active and removes the deleted on timestamp.
        /// </summary>
        /// <param name="id">The id of the entity to be deleted.</param>
        /// <param name="body">The body of the entity to be deleted.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        /// <exception cref="InvalidOperationException">Throws when no entity with <paramref name="id"/> can be found.</exception>
        // Undelete(TEntity entity)
        [Test]
        [TestCase("7ea2d660-1e7b-42d1-b9bb-aa1f1ac96f65", "I was found!")]
        public async Task UndeleteShouldRemoveDeletedOnAndIsDeletedAndUpdateEntity(string id, string body)
        {
            // Arrange
            var repository = new EfDeletableEntityRepository<ReservationNote, Guid>(this.dbContext);
            Guid guid = Guid.Parse(id);
            await this.dbContext.ReservationNotes.AddAsync(new ReservationNote { Id = guid, Body = body });
            await this.dbContext.SaveChangesAsync();

            // Act
            var entity = await this.dbContext
                .ReservationNotes
                .FindAsync(guid)
                ?? throw new InvalidOperationException(string.Format(ErrorMessagesConstants.EntityNotFound, "entity"));
            repository.Undelete(entity);

            // Assert
            Assert.That(this.dbContext.Entry(entity).State, Is.EqualTo(EntityState.Modified));
            await repository.SaveChangesAsync();

            entity = await this.dbContext
                .ReservationNotes
                .FindAsync(guid)
                ?? throw new InvalidOperationException(string.Format(ErrorMessagesConstants.EntityNotFound, "entity"));
            Assert.IsFalse(entity.IsDeleted);
            Assert.That(entity.DeletedOn, Is.Null);
        }
    }
}
