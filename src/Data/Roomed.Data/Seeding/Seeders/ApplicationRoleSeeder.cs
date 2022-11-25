namespace Roomed.Data.Seeding.Seeders
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using Roomed.Data.Models;

    /// <summary>
    /// This is a <see cref="ApplicationRole"/> seeder implementing <see cref="ISeeder"/>.
    /// </summary>
    public class ApplicationRoleSeeder : ISeeder
    {
        /// <inheritdoc/>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<ApplicationRole>>() ?? throw new ArgumentException("Role manager is null.");

            var receptionistRole = new ApplicationRole()
            {
                Name = "Receptionist",
                NormalizedName = "RECEPTIONIST",
            };

            var hotelsManagerRole = new ApplicationRole()
            {
                Name = "HotelsManager",
                NormalizedName = "HOTELSMANAGER",
            };

            if (!await roleManager.RoleExistsAsync("Receptionist"))
            {
                await roleManager.CreateAsync(receptionistRole);
            }

            if (!await roleManager.RoleExistsAsync("HotelsManager"))
            {
                await roleManager.CreateAsync(hotelsManagerRole);
            }
        }
    }
}
