// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ApplicationUserSeeder.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Seeding.Seeders
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using Roomed.Common;
    using Roomed.Data.Models;

    /// <summary>
    /// This is a <see cref="ApplicationUser"/> seeder implementing <see cref="ISeeder"/>.
    /// </summary>
    public class ApplicationUserSeeder : ISeeder
    {
        /// <inheritdoc/>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            Guard.AgainstNull(userManager, nameof(userManager));

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

            ApplicationUser administrator = new ApplicationUser()
            {
                Email = "administrator@mail.com",
                UserName = "administrator",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            if (await userManager.FindByEmailAsync("administrator@mail.com") == null)
            {
                await userManager.CreateAsync(administrator, "administrator123");
            }

            user = await userManager.FindByEmailAsync("administrator@mail.com");

            if (!await userManager.IsInRoleAsync(user, "Administrator"))
            {
                await userManager.AddToRoleAsync(user, "Administrator");
            }

            if (!await userManager.IsInRoleAsync(user, "HotelsManager"))
            {
                await userManager.AddToRoleAsync(user, "HotelsManager");
            }
        }
    }
}
