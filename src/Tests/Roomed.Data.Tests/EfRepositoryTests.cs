// <copyright file="EfRepositoryTests.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// </copyright>

namespace Roomed.Data.Tests
{
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    using Roomed.Data.Models;
    using Roomed.Data.Repositories;
    using Roomed.Tests.Common;

    /// <summary>
    /// This class contains all unit tests for <see cref="EfRepository{TEntity, TKey}"/>.
    /// </summary>
    [TestFixture]
    public class EfRepositoryTests
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
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.All(bool)"/>
        /// returns all entities without tracking.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // All(bool isReadonly = false)
        [Test]
        [Order(1)]
        public async Task AllShouldReturnAllWithoutTracking()
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var entities = await repository.All(true).ToListAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(entities, Is.Not.Empty, "No entities were retrieved.");
                Assert.That(entities, Has.Exactly(this.dbContext.ReservationNotes.Count()).Items, "Entities count is not correct.");

                Assert.That(
                    entities.Select(e => e.Id),
                    Is.EquivalentTo(this.dbContext.ReservationNotes.Select(rn => rn.Id)),
                    "Entities have incorrect ids.");

                Assert.That(
                    entities.Select(e => e.Body),
                    Is.EquivalentTo(this.dbContext.ReservationNotes.Select(rn => rn.Body)),
                    "Entities have incorrect data.");

                Assert.That(entities, Has.All.Matches<ReservationNote>(rn =>
                {
                    var entityState = this.dbContext.Entry(rn).State;

                    return entityState == EntityState.Detached;
                }));
            });
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.All(bool)"/>
        /// returns all entities with tracking.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // All(bool isReadonly = false)
        [Test]
        [Order(2)]
        public async Task AllShouldReturnAllWithTracking()
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var entities = await repository.All().ToListAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(entities, Is.Not.Empty, "No entities were retrieved.");
                Assert.That(entities, Has.Exactly(this.dbContext.ReservationNotes.Count()).Items, "Entities count is not correct.");

                Assert.That(
                    entities.Select(e => e.Id),
                    Is.EquivalentTo(this.dbContext.ReservationNotes.Select(rn => rn.Id)),
                    "Entities have incorrect ids.");

                Assert.That(
                    entities.Select(e => e.Body),
                    Is.EquivalentTo(this.dbContext.ReservationNotes.Select(rn => rn.Body)),
                    "Entities have incorrect data.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.All(System.Linq.Expressions.Expression{Func{TEntity, bool}}, bool)"/>
        /// returns an empty collection when no entity satisfies the condition.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // All(bool isReadonly = false)
        [Test]
        public async Task AllShouldReturnAnEmptyCollection()
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var entities = await repository.All(rn => rn.Body == "No note with this body should exist.").ToListAsync();

            // Assert
            Assert.That(entities, Is.Empty, "The collection contains entities.");
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.All(System.Linq.Expressions.Expression{Func{TEntity, bool}}, bool)"/>
        /// returns all entities satisfying the condition with tracking.
        /// </summary>
        /// <param name="id">The id of the entity with the specified body.</param>
        /// <param name="body">The body of an existing entity.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // All(bool isReadonly = false)
        [Test]
        [TestCase("2bfff802-5afb-4bbb-96b3-27c98161ff00", "Reservation note #1")]
        [TestCase("08bd1b0d-15fd-4d2e-9f59-979d09da1133", "Reservation note #3")]
        public async Task AllShouldReturnAllWithTrackingFiltered(string id, string body)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var entities = await repository.All(rn => rn.Body == body).ToListAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(entities, Is.Not.Empty, "No entities were retrieved.");
                Assert.That(entities, Has.Exactly(1).Items, "Only one entity should be returned.");
                Assert.That(entities[0].Id.ToString(), Is.EqualTo(id), "The entity's id is not correct.");
                Assert.That(entities[0].Body, Is.EqualTo(body), "The entity's data is not correct.");
                Assert.That(this.dbContext.Entry(entities[0]).State, Is.Not.EqualTo(EntityState.Detached), "Entity's state is not tracked.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.All(System.Linq.Expressions.Expression{Func{TEntity, bool}}, bool)"/>
        /// returns all entities satisfying the condition without tracking.
        /// </summary>
        /// <param name="id">The id of the entity with the specified body.</param>
        /// <param name="body">The body of an existing entity.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // All(bool isReadonly = false)
        [Test]
        [TestCase("2bfff802-5afb-4bbb-96b3-27c98161ff00", "Reservation note #1")]
        [TestCase("08bd1b0d-15fd-4d2e-9f59-979d09da1133", "Reservation note #3")]
        public async Task AllShouldReturnAllWithoutTrackingFiltered(string id, string body)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var entities = await repository.All(rn => rn.Body == body, true).ToListAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(entities, Is.Not.Empty, "No entities were retrieved.");
                Assert.That(entities, Has.Exactly(1).Items, "Only one entity should be returned.");
                Assert.That(entities[0].Id.ToString(), Is.EqualTo(id), "The entity's id is not correct.");
                Assert.That(entities[0].Body, Is.EqualTo(body), "The entity's data is not correct.");
                Assert.That(this.dbContext.Entry(entities[0]).State, Is.EqualTo(EntityState.Detached), "Entity's state is not detached.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.Find(TKey, bool)"/>
        /// returns the correct entity without tracking.
        /// </summary>
        /// <param name="id">The id of an existing entity.</param>
        /// <param name="body">The body of the entity.</param>
        // Find(TKey id, bool isReadonly = false)
        [Test]
        [TestCase("2bfff802-5afb-4bbb-96b3-27c98161ff00", "Reservation note #1")]
        [TestCase("08bd1b0d-15fd-4d2e-9f59-979d09da1133", "Reservation note #3")]
        public void FindShouldReturnEntityWithIdWithoutTracking(string id, string body)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var entity = repository.Find(Guid.Parse(id), true);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(entity.Id.ToString(), Is.EqualTo(id), "Entity's id is incorrect.");
                Assert.That(entity.Body, Is.EqualTo(body), "Entity's data is incorrect.");
                Assert.That(this.dbContext.Entry(entity).State, Is.EqualTo(EntityState.Detached), "Entity's state is not detached.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.Find(TKey, bool)"/>
        /// returns the correct entity with tracking.
        /// </summary>
        /// <param name="id">The id of an existing entity.</param>
        /// <param name="body">The body of the entity.</param>
        // Find(TKey id, bool isReadonly = false)
        [Test]
        [TestCase("2bfff802-5afb-4bbb-96b3-27c98161ff00", "Reservation note #1")]
        [TestCase("08bd1b0d-15fd-4d2e-9f59-979d09da1133", "Reservation note #3")]
        public void FindShouldReturnEntityWithIdWithTracking(string id, string body)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var entity = repository.Find(Guid.Parse(id));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(entity.Id.ToString(), Is.EqualTo(id), "Entity's id is incorrect.");
                Assert.That(entity.Body, Is.EqualTo(body), "Entity's data is incorrect.");
                Assert.That(this.dbContext.Entry(entity).State, Is.Not.EqualTo(EntityState.Detached), "Entity's state is not tracked.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.Find(TKey, bool)"/>
        /// throws an error when the entity does not exist.
        /// </summary>
        /// <param name="id">The id of a non existing entity.</param>
        /// Find(TKey id, bool isReadonly = false)
        [Test]
        [TestCase("022f6770-dd45-4c02-b9ed-529134a85595")]
        public void FindShouldThrowWhenEntityDoesNotExist(string id)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            Action code = () => repository.Find(Guid.Parse(id));

            // Assert
            Assert.Throws<InvalidOperationException>(() => code());
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.FindAsync(TKey, bool)"/>
        /// returns the correct entity without tracking.
        /// </summary>
        /// <param name="id">The id of an existing entity.</param>
        /// <param name="body">The body of the entity.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // FindAsync(TKey id, bool isReadonly = false)
        [Test]
        [TestCase("2bfff802-5afb-4bbb-96b3-27c98161ff00", "Reservation note #1")]
        [TestCase("08bd1b0d-15fd-4d2e-9f59-979d09da1133", "Reservation note #3")]
        public async Task FindAsyncShouldReturnEntityWithIdWithoutTracking(string id, string body)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var entity = await repository.FindAsync(Guid.Parse(id), true);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(entity.Id.ToString(), Is.EqualTo(id), "Entity's id is incorrect.");
                Assert.That(entity.Body, Is.EqualTo(body), "Entity's data is incorrect.");
                Assert.That(this.dbContext.Entry(entity).State, Is.EqualTo(EntityState.Detached), "Entity's state is not detached.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.FindAsync(TKey, bool)"/>
        /// returns the correct entity with tracking.
        /// </summary>
        /// <param name="id">The id of an existing entity.</param>
        /// <param name="body">The body of the entity.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // FindAsync(TKey id, bool isReadonly = false)
        [Test]
        [TestCase("2bfff802-5afb-4bbb-96b3-27c98161ff00", "Reservation note #1")]
        [TestCase("08bd1b0d-15fd-4d2e-9f59-979d09da1133", "Reservation note #3")]
        public async Task FindAsyncShouldReturnEntityWithIdWithTracking(string id, string body)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var entity = await repository.FindAsync(Guid.Parse(id));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(entity.Id.ToString(), Is.EqualTo(id), "Entity's id is incorrect.");
                Assert.That(entity.Body, Is.EqualTo(body), "Entity's data is incorrect.");
                Assert.That(this.dbContext.Entry(entity).State, Is.Not.EqualTo(EntityState.Detached), "Entity's state is not tracked.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.FindAsync(TKey, bool)"/>
        /// throws an exception when the entity does not exist.
        /// </summary>
        /// <param name="id">The id of a non existing entity.</param>
        // FindAsync(TKey id, bool isReadonly = false)
        [Test]
        [TestCase("022f6770-dd45-4c02-b9ed-529134a85595")]
        public void FindAsyncShouldThrowWhenEntityDoesNotExist(string id)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            Func<Task> code = async () => await repository.FindAsync(Guid.Parse(id));

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await code(), "Invalid operation exception should be thrown.");
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.Add(TEntity)"/>
        /// adds entity to the database and returns its entity entry.
        /// </summary>
        /// <param name="body">The body of the new entity.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        /// <exception cref="InvalidOperationException">Throws when no entity with returned id can be found.</exception>
        // Add(TEntity entity)
        [Test]
        [TestCase("I was added by Add() method.")]
        public async Task AddShouldAddEntityToDatabaseAndReturnEntityEntry(string body)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var result = repository.Add(new ReservationNote() { Body = body });
            await repository.SaveChangesAsync();

            // Assert
            var entity = await this.dbContext.ReservationNotes.FindAsync(result.Entity.Id)
                ?? throw new InvalidOperationException("Entity can not be found");
            Assert.Multiple(() =>
            {
                Assert.That(entity, Is.Not.Null, "The entity can not be found.");
                Assert.That(entity.CreatedOn, Is.Not.EqualTo(default(DateTime)), "Created should be set.");
                Assert.That(result.Entity.Id, Is.EqualTo(entity.Id), "Ids don't match.");
                Assert.That(body, Is.EqualTo(entity.Body), "Entity data is not set or is not matching.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.AddAsync(TEntity)"/>
        /// adds entity to the database and returns its entity entry asynchronously.
        /// </summary>
        /// <param name="body">The body of the new entity.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        /// <exception cref="InvalidOperationException">Throws when no entity with returned id can be found.</exception>
        // AddAsync(TEntity entity)
        [Test]
        [TestCase("I was added by AddAsync() method.")]
        public async Task AddAsyncShouldAddEntityToDatabaseAndReturnEntityEntry(string body)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var result = await repository.AddAsync(new ReservationNote() { Body = body });
            await repository.SaveChangesAsync();

            // Assert
            var entity = await this.dbContext.ReservationNotes.FindAsync(result.Entity.Id)
                ?? throw new InvalidOperationException("Entity can not be found");
            Assert.Multiple(() =>
            {
                Assert.That(entity, Is.Not.Null, "The entity can not be found.");
                Assert.That(entity.CreatedOn, Is.Not.EqualTo(default(DateTime)), "Created should be set.");
                Assert.That(result.Entity.Id, Is.EqualTo(entity.Id), "Ids don't match.");
                Assert.That(body, Is.EqualTo(entity.Body), "Entity data is not set or is not matching.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.AddRange(IEnumerable{TEntity})"/>
        /// adds multiple entities to the database.
        /// </summary>
        /// <param name="body1">The body of the first entity.</param>
        /// <param name="body2">The body of the second entity.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // AddRange(IEnumerable<TEntity> entities)
        [Test]
        [TestCase("I was added by AddRange() method.", "Me too.")]
        public async Task AddRangeShouldAddEntitiesToDatabase(string body1, string body2)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);
            var notes = new List<ReservationNote>()
            {
                new ReservationNote() { Body = body1 },
                new ReservationNote() { Body = body2 },
            };

            // Act
            repository.AddRange(notes);
            await repository.SaveChangesAsync();

            // Assert
            var entity1 = await this.dbContext
                .ReservationNotes
                .FirstAsync(rn => rn.Body == body1);
            Assert.Multiple(() =>
            {
                Assert.That(entity1, Is.Not.Null, "The entity can not be found.");
                Assert.That(entity1.CreatedOn, Is.Not.EqualTo(default(DateTime)), "Created should be set.");
                Assert.That(body1, Is.EqualTo(entity1.Body), "Entity data is not set or is not matching.");
            });

            var entity2 = await this.dbContext
                .ReservationNotes
                .FirstAsync(rn => rn.Body == body2);

            Assert.Multiple(() =>
            {
                Assert.That(entity2, Is.Not.Null, "The entity can not be found.");
                Assert.That(entity2.CreatedOn, Is.Not.EqualTo(default(DateTime)), "Created should be set.");
                Assert.That(body2, Is.EqualTo(entity2.Body), "Entity data is not set or is not matching.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.AddRangeAsync(IEnumerable{TEntity})"/>
        /// adds multiple entities to the database asynchronously.
        /// </summary>
        /// <param name="body1">The body of the first entity.</param>
        /// <param name="body2">The body of the second entity.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // AddRangeAsync(IEnumerable<TEntity> entities)
        [Test]
        [TestCase("I was added by AddRange() method.", "Me too.")]
        public async Task AddRangeAsyncShouldAddEntitiesToDatabase(string body1, string body2)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);
            var notes = new List<ReservationNote>()
            {
                new ReservationNote() { Body = body1 },
                new ReservationNote() { Body = body2 },
            };

            // Act
            await repository.AddRangeAsync(notes);
            await repository.SaveChangesAsync();

            // Assert
            var entity1 = await this.dbContext
                .ReservationNotes
                .FirstAsync(rn => rn.Body == body1);
            Assert.Multiple(() =>
            {
                Assert.That(entity1, Is.Not.Null, "The entity can not be found.");
                Assert.That(entity1.CreatedOn, Is.Not.EqualTo(default(DateTime)), "Created should be set.");
                Assert.That(body1, Is.EqualTo(entity1.Body), "Entity data is not set or is not matching.");
            });

            var entity2 = await this.dbContext
                .ReservationNotes
                .FirstAsync(rn => rn.Body == body2);
            Assert.Multiple(() =>
            {
                Assert.That(entity2, Is.Not.Null, "The entity can not be found.");
                Assert.That(entity2.CreatedOn, Is.Not.EqualTo(default(DateTime)), "Created should be set.");
                Assert.That(body2, Is.EqualTo(entity2.Body), "Entity data is not set or is not matching.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.Update(TEntity)"/>
        /// modifies the entity's data.
        /// </summary>
        /// <param name="id">The of the entity to be modified.</param>
        /// <param name="body">The original body of the entity.</param>
        /// <param name="modifiedBody">The modified body of the entity.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        /// <exception cref="InvalidOperationException">Throws when no entity with <paramref name="id"/> can be found.</exception>
        // Update(TEntity entity)
        [Test]
        [TestCase("a5652bde-19cd-4ce9-88ff-daf3f7bfd3eb", "My body does not matter because it will be updated.", "I told you ;)")]
        public async Task UpdateShouldUpdateEntity(string id, string body, string modifiedBody)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);
            var guid = Guid.Parse(id);
            await this.dbContext.AddAsync(new ReservationNote() { Id = guid, Body = body });
            await this.dbContext.SaveChangesAsync();

            // Act
            var entity = await this.dbContext.ReservationNotes.FindAsync(guid)
                ?? throw new InvalidOperationException("Entity can not be found.");
            entity.Body = modifiedBody;
            repository.Update(entity);

            // Assert
            Assert.That(this.dbContext.Entry(entity).State, Is.EqualTo(EntityState.Modified), "Entity's state should be Modified.");
            await repository.SaveChangesAsync();

            entity = await this.dbContext.ReservationNotes.FindAsync(guid)
                ?? throw new InvalidOperationException("Entity can not be found");
            Assert.Multiple(() =>
            {
                Assert.That(entity, Is.Not.Null, "Entity can not be found.");
                Assert.That(entity.Body, Is.EqualTo(modifiedBody), "Entity's data is not updated.");
                Assert.That(entity.ModifiedOn, Is.Not.Null, "ModifiedOn should not be null.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.Update(TEntity)"/>
        /// tracks the entity and modifies it.
        /// </summary>
        /// <param name="id">The of the entity to be modified.</param>
        /// <param name="body">The original body of the entity.</param>
        /// <param name="modifiedBody">The modified body of the entity.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        /// <exception cref="InvalidOperationException">Throws when no entity with <paramref name="id"/> can be found.</exception>
        // Update(TEntity entity)
        [Test]
        [TestCase("67a55fdd-269d-474e-a0e8-cba4cbb5f24c", "My body does not matter because it will be updated.", "I told you ;)")]
        public async Task UpdateShouldTrackAndUpdateEntity(string id, string body, string modifiedBody)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);
            var guid = Guid.Parse(id);
            await this.dbContext.AddAsync(new ReservationNote() { Id = guid, Body = body });
            await this.dbContext.SaveChangesAsync();

            // Act
            var entity = await this.dbContext.ReservationNotes.FindAsync(guid) ??
                throw new InvalidOperationException("No entity with specified id can be found.");

            this.dbContext.Entry(entity).State = EntityState.Detached;

            entity.Body = modifiedBody;
            repository.Update(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(this.dbContext.Entry(entity).State, Is.Not.EqualTo(EntityState.Detached), "Entity should be tracked.");
                Assert.That(this.dbContext.Entry(entity).State, Is.EqualTo(EntityState.Modified), "Entity's state should be Modified.");
            });
            await repository.SaveChangesAsync();

            entity = await this.dbContext.ReservationNotes.FindAsync(guid)
                ?? throw new InvalidOperationException("Entity can not be found");
            Assert.Multiple(() =>
            {
                Assert.That(entity, Is.Not.Null, "Entity can not be found.");
                Assert.That(entity.Body, Is.EqualTo(modifiedBody), "Entity's data is not updated.");
                Assert.That(entity.ModifiedOn, Is.Not.Null, "ModifiedOn should not be null.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.UpdateAsync(TKey)"/>
        /// tracks the entity and modifies it asynchronously.
        /// </summary>
        /// <param name="id">The of the entity to be modified.</param>
        /// <param name="body">The original body of the entity.</param>
        /// <param name="modifiedBody">The modified body of the entity.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        /// <exception cref="InvalidOperationException">Throws when no entity with <paramref name="id"/> can be found.</exception>
        // UpdateAsync(TKey id)
        [Test]
        [TestCase("304340a1-16d7-4bdd-b85c-c1521ac3aa2c", "My body does not matter because it will be updated.", "I told you ;)")]
        public async Task UpdateAsyncShouldUpdateEntity(string id, string body, string modifiedBody)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);
            var guid = Guid.Parse(id);
            await this.dbContext.AddAsync(new ReservationNote() { Id = guid, Body = body });
            await this.dbContext.SaveChangesAsync();

            // Act
            var entity = await this.dbContext.ReservationNotes.FindAsync(guid)
                ?? throw new InvalidOperationException("No entity with specified id can be found.");
            entity.Body = modifiedBody;
            await repository.UpdateAsync(guid);

            // Assert
            Assert.That(this.dbContext.Entry(entity).State, Is.EqualTo(EntityState.Modified), "Entity's state should be Modified.");
            await repository.SaveChangesAsync();

            entity = await this.dbContext.ReservationNotes.FindAsync(guid)
                ?? throw new InvalidOperationException("Entity can not be found");
            Assert.Multiple(() =>
            {
                Assert.That(entity, Is.Not.Null, "Entity can not be found.");
                Assert.That(entity.Body, Is.EqualTo(modifiedBody), "Entity's data is not modified.");
                Assert.That(entity.ModifiedOn, Is.Not.Null, "ModifiedOn should not be null.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.UpdateAsync(TKey)"/>
        /// tracks the entity and modifies it asynchronously.
        /// </summary>
        /// <param name="id">The of the entity to be modified.</param>
        /// <param name="body">The original body of the entity.</param>
        /// <param name="modifiedBody">The modified body of the entity.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        /// <exception cref="InvalidOperationException">Throws when no entity with <paramref name="id"/> can be found.</exception>
        // UpdateAsync(TKey id)
        [Test]
        [TestCase("efcd5ace-febb-4999-9530-6726608e6768", "My body does not matter because it will be updated.", "I told you ;)")]
        public async Task UpdateAsyncShouldTrackAndUpdateEntity(string id, string body, string modifiedBody)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);
            var guid = Guid.Parse(id);
            await this.dbContext.AddAsync(new ReservationNote() { Id = guid, Body = body });
            await this.dbContext.SaveChangesAsync();

            // Act
            var entity = await this.dbContext.ReservationNotes.FindAsync(guid)
                ?? throw new InvalidOperationException("Entity can not be found");
            this.dbContext.Entry(entity).State = EntityState.Detached;

            entity.Body = modifiedBody;
            await repository.UpdateAsync(guid);

            // Assert
            entity = await this.dbContext.ReservationNotes.FindAsync(guid)
                ?? throw new InvalidOperationException("Entity can not be found");

            Assert.Multiple(() =>
            {
                Assert.That(this.dbContext.Entry(entity).State, Is.Not.EqualTo(EntityState.Detached), "Entity should be tracked.");
                Assert.That(this.dbContext.Entry(entity).State, Is.EqualTo(EntityState.Modified), "Entity's state should be Modified.");
            });

            entity.Body = modifiedBody;
            await repository.SaveChangesAsync();

            entity = await this.dbContext.ReservationNotes.FindAsync(guid)
                ?? throw new InvalidOperationException("Entity can not be found");
            Assert.Multiple(() =>
            {
                Assert.That(entity, Is.Not.Null, "Entity can not be found.");
                Assert.That(entity.Body, Is.EqualTo(modifiedBody), "Entity's data is not modified.");
                Assert.That(entity.ModifiedOn, Is.Not.Null, "ModifiedOn should not be null.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.UpdateAsync(TKey)"/>
        /// throws an exception when the entity does not exist.
        /// </summary>
        /// <param name="id">The id of a non existing entity.</param>
        // UpdateAsync(TKey id)
        [Test]
        [TestCase("1626c009-2a86-4516-818f-44dc4ae17b0a")]
        public void UpdateAsyncThrowsWhenEntityDoesNotExist(string id)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            Func<Task> code = async () => await repository.UpdateAsync(Guid.Parse(id));

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await code(), "Invalid operation exception should be thrown.");
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.UpdateRange(IEnumerable{TEntity})"/>
        /// updates a collection of entities.
        /// </summary>
        /// <param name="body1">The original body of the first entity.</param>
        /// <param name="body2">The original body of the second entity.</param>
        /// <param name="addition">The addition text added to both entities.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // UpdateRange(IEnumerable<TEntity> entities)
        [Test]
        [TestCase("I will be updated!", "I think I'm going to be updated too!", "Yes")]
        public async Task UpdateRangeShouldUpdateAllEntities(string body1, string body2, string addition)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);
            var notes = new List<ReservationNote>()
            {
                new ReservationNote() { Body = body1 },
                new ReservationNote() { Body = body2 },
            };
            await repository.AddRangeAsync(notes);
            await repository.SaveChangesAsync();

            // Act
            var entity1 = await this.dbContext
                .ReservationNotes
                .FirstAsync(rn => rn.Body == body1);
            entity1.Body += addition;
            var entity2 = await this.dbContext
                .ReservationNotes
                .FirstAsync(rn => rn.Body == body2);
            entity2.Body += addition;

            repository.UpdateRange(new List<ReservationNote>() { entity1, entity2 });
            await repository.SaveChangesAsync();

            // Assert
            entity1 = await this.dbContext
                .ReservationNotes
                .FirstAsync(rn => rn.Body == body1 + addition);
            Assert.Multiple(() =>
            {
                Assert.That(entity1, Is.Not.Null, "The entity can not be found.");
                Assert.That(entity1.ModifiedOn, Is.Not.Null, "ModifiedOn should not be null.");
                Assert.That(entity1.Body, Is.EqualTo(body1 + addition), "Entity data is not set or is not matching.");
            });
            entity2 = await this.dbContext
                .ReservationNotes
                .FirstAsync(rn => rn.Body == body2 + addition);
            Assert.Multiple(() =>
            {
                Assert.That(entity2, Is.Not.Null, "The entity can not be found.");
                Assert.That(entity2.ModifiedOn, Is.Not.Null, "ModifiedOn should not be null.");
                Assert.That(entity2.Body, Is.EqualTo(body2 + addition), "Entity data is not set or is not matching.");
            });
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.Delete(TEntity)"/>
        /// changes entity's state and deletes it.
        /// </summary>
        /// <param name="id">The id of the new entity.</param>
        /// <param name="body">The body of the new entity.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        /// <exception cref="InvalidOperationException">Throws when the entity can not be found.</exception>
        // Delete(TEntity entity)
        [Test]
        [TestCase("d429e67c-d9d7-4a93-9ba7-e35ad2150182", "I'm going to be deleted :(")]
        public async Task DeleteShouldChangeEntityStateAndDeleteIt(string id, string body)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);
            var guid = Guid.Parse(id);
            await this.dbContext.AddAsync(new ReservationNote() { Id = guid, Body = body });
            await this.dbContext.SaveChangesAsync();

            // Act
            var entity = await this.dbContext.ReservationNotes.FindAsync(guid)
                ?? throw new InvalidOperationException("Entity can not be found");
            repository.Delete(entity);

            // Assert
            Assert.That(this.dbContext.Entry(entity).State, Is.EqualTo(EntityState.Deleted), "Entity's state should be Deleted.");
            await repository.SaveChangesAsync();

            entity = await this.dbContext.ReservationNotes.FirstOrDefaultAsync(rn => rn.Id == guid);
            Assert.That(entity, Is.Null, "Entity is not deleted.");
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.DeleteAsync(TKey)"/>
        /// changes entity's state and deletes it asynchronously.
        /// </summary>
        /// <param name="id">The id of the new entity.</param>
        /// <param name="body">The body of the new entity.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        /// <exception cref="InvalidOperationException">Throws when the entity can not be found.</exception>
        // DeleteAsync(TKey id)
        [Test]
        [TestCase("3edef0b0-912d-497a-ae8c-d183b898d627", "I'm going to be deleted :(")]
        public async Task DeleteAsyncShouldChangeEntityStateAndDeleteIt(string id, string body)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);
            var guid = Guid.Parse(id);
            await this.dbContext.AddAsync(new ReservationNote() { Id = guid, Body = body });
            await this.dbContext.SaveChangesAsync();

            // Act
            var entity = await this.dbContext.ReservationNotes.FindAsync(guid)
                ?? throw new InvalidOperationException("Entity can not be found");
            await repository.DeleteAsync(guid);

            // Assert
            Assert.That(this.dbContext.Entry(entity).State, Is.EqualTo(EntityState.Deleted), "Entity's state should be Deleted.");
            await repository.SaveChangesAsync();

            entity = await this.dbContext.ReservationNotes.FirstOrDefaultAsync(rn => rn.Id == guid);
            Assert.That(entity, Is.Null, "Entity is not deleted.");
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.DeleteAsync(TKey)"/>
        /// throws when the entity does not exist.
        /// </summary>
        /// <param name="id">The id of a non existing entity.</param>
        /// DeleteAsync(TKey id)
        [Test]
        [TestCase("0afc3501-cdfe-4d2e-91a4-15a41ac0d5db")]
        public void DeleteAsyncShouldThrowWhenEntityDoesNotExist(string id)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            Func<Task> code = async () => await repository.DeleteAsync(Guid.Parse(id));

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await code(), "Invalid operation exception should be thrown.");
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.DeleteRange(IEnumerable{TEntity})"/>
        /// changes the state of a collection of entities and deletes them.
        /// </summary>
        /// <param name="body1">The body of the first entity.</param>
        /// <param name="body2">The body of the second entity.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        // DeleteRange(IEnumerable<TEntity> entities)
        [Test]
        [TestCase("We are going to be deleted!?", "I don't like where this is going")]
        public async Task DeleteRangeShouldRemoveEntities(string body1, string body2)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);
            var notes = new List<ReservationNote>()
            {
                new ReservationNote() { Body = body1 },
                new ReservationNote() { Body = body2 },
            };
            await repository.AddRangeAsync(notes);
            await repository.SaveChangesAsync();

            // Act
            var entity1 = await this.dbContext
                .ReservationNotes
                .FirstAsync(rn => rn.Body == body1);
            var entity2 = await this.dbContext
                .ReservationNotes
                .FirstAsync(rn => rn.Body == body2);

            repository.DeleteRange(new List<ReservationNote>() { entity1, entity2 });

            // Assert
            entity1 = await this.dbContext
                .ReservationNotes
                .FirstAsync(rn => rn.Body == body1);
            entity2 = await this.dbContext
                .ReservationNotes
                .FirstAsync(rn => rn.Body == body2);
            Assert.Multiple(() =>
            {
                Assert.That(this.dbContext.Entry(entity1).State, Is.EqualTo(EntityState.Deleted), "Entity's state should be Deleted.");
                Assert.That(this.dbContext.Entry(entity2).State, Is.EqualTo(EntityState.Deleted), "Entity's state should be Deleted.");
            });
            await repository.SaveChangesAsync();

            entity1 = await this.dbContext
                .ReservationNotes
                .FirstOrDefaultAsync(rn => rn.Body == body1);
            Assert.That(entity1, Is.Null, "The entity is not deleted.");

            entity2 = await this.dbContext
                .ReservationNotes
                .FirstOrDefaultAsync(rn => rn.Body == body2);
            Assert.That(entity2, Is.Null, "The entity is not deleted.");
        }

        /// <summary>
        /// This test checks whether <see cref="EfRepository{TEntity, TKey}.Detach(TEntity)"/>
        /// sets the entity's state to detached.
        /// </summary>
        /// <param name="id">The id of the entity to be detached.</param>
        /// <param name="body">The body of entity.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        /// <exception cref="InvalidOperationException">Throws when the entity can not be found.</exception>
        // Detach(TEntity entity)
        [Test]
        [TestCase("9a14c00e-dbd6-4b05-9541-2af3f5ec6f31", "Am I detached?")]
        public async Task DetachShouldStopTrackingEntity(string id, string body)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);
            var guid = Guid.Parse(id);
            await this.dbContext.AddAsync(new ReservationNote() { Id = guid, Body = body });
            await this.dbContext.SaveChangesAsync();

            // Act
            var entity = await this.dbContext.ReservationNotes.FindAsync(guid)
                ?? throw new InvalidOperationException("Entity can not be found");
            this.dbContext.Entry(entity).State = EntityState.Modified;
            repository.Detach(entity);

            // Assert
            Assert.That(this.dbContext.Entry(entity).State, Is.Not.EqualTo(EntityState.Modified), "Entity's state should be changed.");
            Assert.That(this.dbContext.Entry(entity).State, Is.EqualTo(EntityState.Detached), "Entity's state should be Deleted.");
        }
    }
}
