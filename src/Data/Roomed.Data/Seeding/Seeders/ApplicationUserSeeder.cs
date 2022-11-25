﻿namespace Roomed.Data.Seeding.Seeders
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using Roomed.Data.Models;

    /// <summary>
    /// This is a <see cref="ApplicationUser"/> seeder implementing <see cref="ISeeder"/>.
    /// </summary>
    public class ApplicationUserSeeder : ISeeder
    {
        /// <inheritdoc/>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>() ?? throw new ArgumentException("User manager is null.");

            ApplicationUser receptionist = new ApplicationUser()
            {
                Email = "receptionist@mail.com",
                UserName = "receptionist",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            if (await userManager.FindByEmailAsync("receptionist@mail.com") == null)
            {
                await userManager.CreateAsync(receptionist, "receptionist123");
            }

            var user = await userManager.FindByEmailAsync("receptionist@mail.com");

            if (!await userManager.IsInRoleAsync(user, "Receptionist"))
            {
                await userManager.AddToRoleAsync(user, "Receptionist");
            }

            ApplicationUser hotelsManager = new ApplicationUser()
            {
                Email = "hotelsManager@mail.com",
                UserName = "hotelsManager",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            if (await userManager.FindByEmailAsync("hotelsManager@mail.com") == null)
            {
                await userManager.CreateAsync(hotelsManager, "hotelsManager123");
            }

            user = await userManager.FindByEmailAsync("hotelsManager@mail.com");

            if (!await userManager.IsInRoleAsync(user, "HotelsManager"))
            {
                await userManager.AddToRoleAsync(user, "HotelsManager");
            }
        }
    }
}