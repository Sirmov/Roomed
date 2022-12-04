namespace Roomed.Data.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Roomed.Data.Models;

    [TestFixture]
    public class EfDeletableRepositoryTests
    {
        private ReservationNote[] reservationNotes;
        private string[] guids;
        private string[] bodies;

        private ApplicationDbContext dbContext;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            this.guids = new string[]
                { "2bfff802-5afb-4bbb-96b3-27c98161ff00", "bb2f7b6c-d8d9-4e2c-b14b-3bd98e18ad86", "08bd1b0d-15fd-4d2e-9f59-979d09da1133" };
            this.bodies = new string[]
                { "Reservation note #1", "Reservation note #2", "Reservation note #3" };
            this.reservationNotes = new ReservationNote[this.guids.Length];

            for (int i = 0; i < this.guids.Length; i++)
            {
                var id = Guid.Parse(this.guids[i]);
                var body = this.bodies[i];
                this.reservationNotes[i] = new ReservationNote() { Id = id, Body = body };
            }

            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("RoomedInMemory")
                .Options;

            this.dbContext = new ApplicationDbContext(options);
            await this.dbContext.AddRangeAsync(reservationNotes);
            await this.dbContext.SaveChangesAsync();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await this.dbContext.DisposeAsync();
        }

        // All

        [Test]
        public async Task AllShouldReturnAllEntitiesIncludingDeleted()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task AllShouldReturnAllNotDeletedEntities()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task AllShouldReturnAllFilteredNotDeletedEntities()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task AllShouldReturnAllFilteredNotDeletedEntitiesWithoutTracking()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        // HardDelete

        [Test]
        public async Task HardDeleteShouldDeleteEntityFromDatabase()
        {
            // Arrange

            // Act (detached entity)

            // Assert
            Assert.Fail();
        }

        // Delete

        [Test]
        public async Task DeleteShouldSetIsDeletedFlagUpdateEntityAndSetDeletedOn()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        // Undelete

        [Test]
        public async Task UndeleteShouldRemoveDeletedOnAndIsDeletedAndUpdateEntity()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}
