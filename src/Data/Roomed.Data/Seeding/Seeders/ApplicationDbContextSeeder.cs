namespace Roomed.Data.Seeding.Seeders
{
    using System;

    /// <summary>
    /// This is a application db context seeder. It is used to seed the database with all custom db context seeders implementing <see cref="ISeeder"/>.
    /// </summary>
    public static class ApplicationDbContextSeeder
    {
        /// <summary>
        /// This method invokes all SeedAsync methods from all custom db context seeders.
        /// They are invoked in a strictly specific order to prevent foreign key constraint violations.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="serviceProvider">The IoC container, service provider.</param>
        /// <returns>Returns <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">Throws when dbContext or serviceProvider arguments are null.</exception>
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
                            new ApplicationRoleSeeder(),
                            new ApplicationUserSeeder(),
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
