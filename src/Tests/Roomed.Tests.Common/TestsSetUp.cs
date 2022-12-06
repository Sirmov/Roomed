namespace Roomed.Tests.Common
{
    using Microsoft.EntityFrameworkCore;
    using Roomed.Data;
    using Roomed.Data.Models;

    public static class TestsSetUp
    {
        private static readonly ICollection<ReservationNote> reservationNotes = new List<ReservationNote>
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

        public static async Task<ApplicationDbContext> InitializeDbContextAsync()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("RoomedInMemory")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            if (dbContext != null)
            {
                await dbContext.Database.EnsureDeletedAsync();
            }

            await dbContext.AddRangeAsync(reservationNotes);
            await dbContext.SaveChangesAsync();

            return dbContext;
        }
    }
}