// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ApplicationRoleSeeder.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

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

            var administratorRole = new ApplicationRole()
            {
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
            };

            if (!await roleManager.RoleExistsAsync("Receptionist"))
            {
                await roleManager.CreateAsync(receptionistRole);
            }

            if (!await roleManager.RoleExistsAsync("HotelsManager"))
            {
                await roleManager.CreateAsync(hotelsManagerRole);
            }

            if (!await roleManager.RoleExistsAsync("Administrator"))
            {
                await roleManager.CreateAsync(administratorRole);
            }
        }
    }
}
