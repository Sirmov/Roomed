namespace Roomed.Data.Tests
{
    using Microsoft.EntityFrameworkCore;

    using Roomed.Data.Models;
    using Roomed.Data.Repositories;

    [TestFixture]
    public class EfDeletableRepositoryTests
    {
        private ApplicationDbContext dbContext;

        [SetUp]
        public async Task Setup()
        {
            this.dbContext = await InitializeDbContextAsync();
        }

        [TearDown]
        public async Task OneTimeTearDown()
        {
            await this.dbContext.Database.EnsureDeletedAsync();
            await this.dbContext.DisposeAsync();
        }

        // All

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

        [Test]
        public async Task AllShouldReturnAllNotDeletedEntities()
        {
            // Arrange
            var repository = new EfDeletableEntityRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var entities = await repository.All().ToListAsync();

            // Assert
            Assert.That(entities.Count,
                Is.EqualTo(this.dbContext.ReservationNotes.Count(x => !x.IsDeleted)), "Entities count is not correct.");
            Assert.That(entities.Any(rn => rn.IsDeleted), Is.False, "Only not deleted entities should be returned");
        }

        [Test]
        public async Task AllShouldReturnAllFilteredNotDeletedEntities()
        {
            // Arrange
            var repository = new EfDeletableEntityRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var entities = await repository.All(rn => rn.Body.Contains("Reservation note")).ToListAsync();

            // Assert
            Assert.That(entities.Count,
                Is.EqualTo(this.dbContext.ReservationNotes.Count(x => !x.IsDeleted)), "Entities count is not correct.");
            Assert.That(entities.Any(rn => rn.IsDeleted), Is.False, "Only not deleted entities should be returned");
        }

        [Test]
        public async Task AllShouldReturnAllFilteredNotDeletedEntitiesWithoutTracking()
        {
            // Arrange
            var repository = new EfDeletableEntityRepository<ReservationNote, Guid>(this.dbContext);

            // Act
            var entities = await repository.All(rn => rn.Body.Contains("Reservation note"), true).ToListAsync();

            // Assert
            Assert.That(entities.Count,
                Is.EqualTo(this.dbContext.ReservationNotes.Count(x => !x.IsDeleted)), "Entities count is not correct.");
            Assert.That(entities.Any(rn => rn.IsDeleted), Is.False, "Only not deleted entities should be returned");
            Assert.That(entities.All(rn => this.dbContext.Entry(rn).State == EntityState.Detached), Is.True);
        }

        // HardDelete

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
                .FindAsync(guid);

            repository.HardDelete(entity);
            
            // Assert
            Assert.IsTrue(this.dbContext.Entry(entity).State == EntityState.Deleted);
            await repository.SaveChangesAsync();

            var entities = await this.dbContext.ReservationNotes.ToListAsync();
            Assert.That(entities.Any(rn => rn.Id == guid), Is.False);
            Assert.That(entities.Any(rn => rn.Body == body), Is.False);
        }

        // Delete

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
                .FindAsync(guid);
            repository.Delete(entity);

            // Assert
            Assert.IsTrue(this.dbContext.Entry(entity).State == EntityState.Modified);
            await repository.SaveChangesAsync();

            entity = await this.dbContext
                .ReservationNotes
                .FindAsync(guid);
            Assert.IsTrue(entity.IsDeleted);
            Assert.That(entity.DeletedOn, Is.Not.Null);
        }

        // Undelete

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
                .FindAsync(guid);
            repository.Undelete(entity);

            // Assert
            Assert.IsTrue(this.dbContext.Entry(entity).State == EntityState.Modified);
            await repository.SaveChangesAsync();

            entity = await this.dbContext
                .ReservationNotes
                .FindAsync(guid);
            Assert.IsFalse(entity.IsDeleted);
            Assert.That(entity.DeletedOn, Is.Null);
        }
    }
}
