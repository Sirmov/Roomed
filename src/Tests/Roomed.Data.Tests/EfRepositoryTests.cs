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

    [TestFixture]
    public class EfRepositoryTests
    {
        private ApplicationDbContext dbContext;

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
        };

        [SetUp]
        public async Task Setup()
        {
            this.dbContext = await DbContextMock.InitializeDbContextAsync();
            await this.dbContext.AddRangeAsync(this.reservationNotes);
            await this.dbContext.SaveChangesAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await DbContextMock.DisposeAsync();
        }

        // All

        [Test, Order(1)]
        public async Task AllShouldReturnAllWithoutTracking()
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var entities = await repository.All(true).ToListAsync();

            // Assert
            Assert.That(entities, Is.Not.Empty, "No entities were retrieved.");
            Assert.That(entities, Has.Exactly(this.dbContext.ReservationNotes.Count()).Items, "Entities count is not correct.");
            Assert.That(entities.Select(e => e.Id),
                Is.EquivalentTo(this.dbContext.ReservationNotes.Select(rn => rn.Id)), "Entities have incorrect ids.");
            Assert.That(entities.Select(e => e.Body),
                Is.EquivalentTo(this.dbContext.ReservationNotes.Select(rn => rn.Body)), "Entities have incorrect data.");
            Assert.That(entities, Has.All.Matches<ReservationNote>(rn =>
            {
                var entityState = this.dbContext.Entry(rn).State;

                return entityState == EntityState.Detached;
            }));
        }

        [Test, Order(2)]
        public async Task AllShouldReturnAllWithTracking()
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var entities = await repository.All().ToListAsync();

            // Assert
            Assert.That(entities, Is.Not.Empty, "No entities were retrieved.");
            Assert.That(entities, Has.Exactly(this.dbContext.ReservationNotes.Count()).Items, "Entities count is not correct.");
            Assert.That(entities.Select(e => e.Id),
                Is.EquivalentTo(this.dbContext.ReservationNotes.Select(rn => rn.Id)), "Entities have incorrect ids.");
            Assert.That(entities.Select(e => e.Body),
                Is.EquivalentTo(this.dbContext.ReservationNotes.Select(rn => rn.Body)), "Entities have incorrect data.");
        }

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
            Assert.That(entities, Is.Not.Empty, "No entities were retrieved.");
            Assert.That(entities, Has.Exactly(1).Items, "Only one entity should be returned.");
            Assert.That(entities[0].Id.ToString() == id, "The entity's id is not correct.");
            Assert.IsTrue(entities[0].Body == body, "The entity's data is not correct.");
            Assert.IsTrue(this.dbContext.Entry(entities[0]).State != EntityState.Detached, "Entity's state is not tracked.");
        }

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
            Assert.That(entities, Is.Not.Empty, "No entities were retrieved.");
            Assert.That(entities, Has.Exactly(1).Items, "Only one entity should be returned.");
            Assert.IsTrue(entities[0].Id.ToString() == id, "The entity's id is not correct.");
            Assert.IsTrue(entities[0].Body == body, "The entity's data is not correct.");
            Assert.IsTrue(this.dbContext.Entry(entities[0]).State == EntityState.Detached, "Entity's state is not detached.");
        }

        // Find

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
            Assert.IsTrue(entity.Id.ToString() == id, "Entity's id is incorrect.");
            Assert.IsTrue(entity.Body == body, "Entity's data is incorrect.");
            Assert.IsTrue(this.dbContext.Entry(entity).State == EntityState.Detached, "Entity's state is not detached.");
        }

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
            Assert.IsTrue(entity.Id.ToString() == id, "Entity's id is incorrect.");
            Assert.IsTrue(entity.Body == body, "Entity's data is incorrect.");
            Assert.IsTrue(this.dbContext.Entry(entity).State != EntityState.Detached, "Entity's state is not tracked.");
        }

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

        // FindAsync

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
            Assert.IsTrue(entity.Id.ToString() == id, "Entity's id is incorrect.");
            Assert.IsTrue(entity.Body == body, "Entity's data is incorrect.");
            Assert.IsTrue(this.dbContext.Entry(entity).State == EntityState.Detached, "Entity's state is not detached.");
        }

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
            Assert.IsTrue(entity.Id.ToString() == id, "Entity's id is incorrect.");
            Assert.IsTrue(entity.Body == body, "Entity's data is incorrect.");
            Assert.IsTrue(this.dbContext.Entry(entity).State != EntityState.Detached, "Entity's state is not tracked.");
        }

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

        // Add

        [Test]
        [TestCase("I was added by Add() method.")]
        public async Task AddShouldAddEntityToDatabaseAndReturnEntityEntry(string body)
        {
            // Arrange
            var repository = new EfRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var result = repository.Add(new ReservationNote() { Body = body});
            await repository.SaveChangesAsync();

            // Assert
            var entity = await this.dbContext.ReservationNotes.FindAsync(result.Entity.Id);
            Assert.That(entity, Is.Not.Null, "The entity can not be found.");
            Assert.That(entity.CreatedOn, Is.Not.EqualTo(default(DateTime)), "Created should be set.");
            Assert.That(result.Entity.Id, Is.EqualTo(entity.Id), "Ids don't match.");
            Assert.That(body, Is.EqualTo(entity.Body), "Entity data is not set or is not matching.");
        }

        // AddAsync

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
            var entity = await this.dbContext.ReservationNotes.FindAsync(result.Entity.Id);
            Assert.That(entity, Is.Not.Null, "The entity can not be found.");
            Assert.That(entity.CreatedOn, Is.Not.EqualTo(default(DateTime)), "Created should be set.");
            Assert.That(result.Entity.Id, Is.EqualTo(entity.Id), "Ids don't match.");
            Assert.That(body, Is.EqualTo(entity.Body), "Entity data is not set or is not matching.");
        }

        // AddRange

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
                .FirstOrDefaultAsync(rn => rn.Body == body1);
            Assert.That(entity1, Is.Not.Null, "The entity can not be found.");
            Assert.That(entity1.CreatedOn, Is.Not.EqualTo(default(DateTime)), "Created should be set.");
            Assert.That(body1, Is.EqualTo(entity1.Body), "Entity data is not set or is not matching.");

            var entity2 = await this.dbContext
                .ReservationNotes
                .FirstOrDefaultAsync(rn => rn.Body == body2);
            Assert.That(entity2, Is.Not.Null, "The entity can not be found.");
            Assert.That(entity2.CreatedOn, Is.Not.EqualTo(default(DateTime)), "Created should be set.");
            Assert.That(body2, Is.EqualTo(entity2.Body), "Entity data is not set or is not matching.");
        }

        // AddRangeAsync

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
                .FirstOrDefaultAsync(rn => rn.Body == body1);
            Assert.That(entity1, Is.Not.Null, "The entity can not be found.");
            Assert.That(entity1.CreatedOn, Is.Not.EqualTo(default(DateTime)), "Created should be set.");
            Assert.That(body1, Is.EqualTo(entity1.Body), "Entity data is not set or is not matching.");

            var entity2 = await this.dbContext
                .ReservationNotes
                .FirstOrDefaultAsync(rn => rn.Body == body2);
            Assert.That(entity2, Is.Not.Null, "The entity can not be found.");
            Assert.That(entity2.CreatedOn, Is.Not.EqualTo(default(DateTime)), "Created should be set.");
            Assert.That(body2, Is.EqualTo(entity2.Body), "Entity data is not set or is not matching.");
        }

        // Update

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
            var entity = await this.dbContext.ReservationNotes.FindAsync(guid);
            entity.Body = modifiedBody;
            repository.Update(entity);

            // Assert
            Assert.That(this.dbContext.Entry(entity).State, Is.EqualTo(EntityState.Modified), "Entity's state should be Modified.");
            await repository.SaveChangesAsync();

            entity = await this.dbContext.ReservationNotes.FindAsync(guid);
            Assert.That(entity, Is.Not.Null, "Entity can not be found.");
            Assert.That(entity.Body, Is.EqualTo(modifiedBody), "Entity's data is not updated.");
            Assert.That(entity.ModifiedOn, Is.Not.Null, "ModifiedOn should not be null.");
        }

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
            var entity = await this.dbContext.ReservationNotes.FindAsync(guid);
            this.dbContext.Entry(entity).State = EntityState.Detached;

            entity.Body = modifiedBody;
            repository.Update(entity);

            // Assert
            Assert.That(this.dbContext.Entry(entity).State, Is.Not.EqualTo(EntityState.Detached), "Entity should be tracked.");
            Assert.That(this.dbContext.Entry(entity).State, Is.EqualTo(EntityState.Modified), "Entity's state should be Modified.");
            await repository.SaveChangesAsync();

            entity = await this.dbContext.ReservationNotes.FindAsync(guid);
            Assert.That(entity, Is.Not.Null, "Entity can not be found.");
            Assert.That(entity.Body, Is.EqualTo(modifiedBody), "Entity's data is not updated.");
            Assert.That(entity.ModifiedOn, Is.Not.Null, "ModifiedOn should not be null.");
        }

        // UpdateAsync

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
            var entity = await this.dbContext.ReservationNotes.FindAsync(guid);
            entity.Body = modifiedBody;
            await repository.UpdateAsync(guid);

            // Assert
            Assert.That(this.dbContext.Entry(entity).State, Is.EqualTo(EntityState.Modified), "Entity's state should be Modified.");
            await repository.SaveChangesAsync();

            entity = await this.dbContext.ReservationNotes.FindAsync(guid);
            Assert.That(entity, Is.Not.Null, "Entity can not be found.");
            Assert.That(entity.Body, Is.EqualTo(modifiedBody), "Entity's data is not modified.");
            Assert.That(entity.ModifiedOn, Is.Not.Null, "ModifiedOn should not be null.");
        }

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
            var entity = await this.dbContext.ReservationNotes.FindAsync(guid);
            this.dbContext.Entry(entity).State = EntityState.Detached;

            entity.Body = modifiedBody;
            await repository.UpdateAsync(guid);

            // Assert
            entity = await this.dbContext.ReservationNotes.FindAsync(guid);
            Assert.That(this.dbContext.Entry(entity).State, Is.Not.EqualTo(EntityState.Detached), "Entity should be tracked.");
            Assert.That(this.dbContext.Entry(entity).State, Is.EqualTo(EntityState.Modified), "Entity's state should be Modified.");
            entity.Body = modifiedBody;
            await repository.SaveChangesAsync();

            entity = await this.dbContext.ReservationNotes.FindAsync(guid);
            Assert.That(entity, Is.Not.Null, "Entity can not be found.");
            Assert.That(entity.Body, Is.EqualTo(modifiedBody), "Entity's data is not modified.");
            Assert.That(entity.ModifiedOn, Is.Not.Null, "ModifiedOn should not be null.");
        }

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

        // UpdateRange

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
                .FirstOrDefaultAsync(rn => rn.Body == body1);
            entity1.Body += addition;
            var entity2 = await this.dbContext
                .ReservationNotes
                .FirstOrDefaultAsync(rn => rn.Body == body2);
            entity2.Body += addition;

            repository.UpdateRange(new List<ReservationNote>() { entity1, entity2 });
            await repository.SaveChangesAsync();

            // Assert
            entity1 = await this.dbContext
                .ReservationNotes
                .FirstOrDefaultAsync(rn => rn.Body == body1 + addition);
            Assert.That(entity1, Is.Not.Null, "The entity can not be found.");
            Assert.That(entity1.ModifiedOn, Is.Not.Null, "ModifiedOn should not be null.");
            Assert.That(entity1.Body, Is.EqualTo(body1 + addition), "Entity data is not set or is not matching.");

            entity2 = await this.dbContext
                .ReservationNotes
                .FirstOrDefaultAsync(rn => rn.Body == body2 + addition);
            Assert.That(entity2, Is.Not.Null, "The entity can not be found.");
            Assert.That(entity2.ModifiedOn, Is.Not.Null, "ModifiedOn should not be null.");
            Assert.That(entity2.Body, Is.EqualTo(body2 + addition), "Entity data is not set or is not matching.");
        }

        // Delete

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
            var entity = await this.dbContext.ReservationNotes.FindAsync(guid);
            repository.Delete(entity);

            // Assert
            Assert.That(this.dbContext.Entry(entity).State, Is.EqualTo(EntityState.Deleted), "Entity's state should be Deleted.");
            await repository.SaveChangesAsync();

            entity = await this.dbContext.ReservationNotes.FirstOrDefaultAsync(rn => rn.Id == guid);
            Assert.That(entity, Is.Null, "Entity is not deleted.");
        }

        // DeleteAsync

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
            var entity = await this.dbContext.ReservationNotes.FindAsync(guid);
            await repository.DeleteAsync(guid);

            // Assert
            Assert.That(this.dbContext.Entry(entity).State, Is.EqualTo(EntityState.Deleted), "Entity's state should be Deleted.");
            await repository.SaveChangesAsync();

            entity = await this.dbContext.ReservationNotes.FirstOrDefaultAsync(rn => rn.Id == guid);
            Assert.That(entity, Is.Null, "Entity is not deleted.");
        }

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

        // DeleteRange

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
                .FirstOrDefaultAsync(rn => rn.Body == body1);
            var entity2 = await this.dbContext
                .ReservationNotes
                .FirstOrDefaultAsync(rn => rn.Body == body2);

            repository.DeleteRange(new List<ReservationNote>() { entity1, entity2 });

            // Assert
            entity1 = await this.dbContext
                .ReservationNotes
                .FirstOrDefaultAsync(rn => rn.Body == body1);
            entity2 = await this.dbContext
                .ReservationNotes
                .FirstOrDefaultAsync(rn => rn.Body == body2);

            Assert.That(this.dbContext.Entry(entity1).State, Is.EqualTo(EntityState.Deleted), "Entity's state should be Deleted.");
            Assert.That(this.dbContext.Entry(entity2).State, Is.EqualTo(EntityState.Deleted), "Entity's state should be Deleted.");
            await repository.SaveChangesAsync();

            entity1 = await this.dbContext
                .ReservationNotes
                .FirstOrDefaultAsync(rn => rn.Body == body1);
            Assert.That(entity1, Is.Null, "The entity is not deleted.");

            entity2 = await this.dbContext
                .ReservationNotes
                .FirstOrDefaultAsync(rn => rn.Body == body2 );
            Assert.That(entity2, Is.Null, "The entity is not deleted.");
        }

        // Detach

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
            var entity = await this.dbContext.ReservationNotes.FindAsync(guid);
            this.dbContext.Entry(entity).State = EntityState.Modified;
            repository.Detach(entity);

            // Assert
            Assert.That(this.dbContext.Entry(entity).State, Is.Not.EqualTo(EntityState.Modified), "Entity's state should be changed.");
            Assert.That(this.dbContext.Entry(entity).State, Is.EqualTo(EntityState.Detached), "Entity's state should be Deleted.");
        }
    }
}
