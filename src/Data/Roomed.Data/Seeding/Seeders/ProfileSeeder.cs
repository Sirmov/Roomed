// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ProfileSeeder.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Seeding.Seeders
{
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    using Roomed.Data.Models;
    using Roomed.Services.Json.SerializerSettings;

    /// <summary>
    /// This is a <see cref="Profile"/> seeder implementing <see cref="ISeeder"/>.
    /// </summary>
    public class ProfileSeeder : ISeeder
    {
        /// <inheritdoc/>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            string json = await File.ReadAllTextAsync("../../Data/Roomed.Data/Seeding/Data/ProfileSeed.json");

            var profiles = JsonConvert.DeserializeObject<IEnumerable<Profile>>(json, new DateOnlyJsonSettings().Settings)
                ?? throw new InvalidOperationException("Deserialization was not successful.");

            foreach (var profile in profiles)
            {
                if (!(await dbContext.Profiles.AnyAsync(p => p.Id == profile.Id)))
                {
                    await dbContext.Profiles.AddAsync(profile);
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
