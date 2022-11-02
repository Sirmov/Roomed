namespace Roomed.Data.Seeding.Seeders
{
    using System;

    public static class ApplicationDbContextSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var seeders = new List<ISeeder>
                        {
                            new RoomTypeSeeder(),
                            new RoomSeeder(),
                            new ProfileSeeder(),
                            new IdentityDocumentSeeder(),
                            new ReservationSeeder(),
                            new ReservationDaySeeder(),
                            new ReservationDayGuestSeeder(),
                        };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
            }
        }
    }
}
