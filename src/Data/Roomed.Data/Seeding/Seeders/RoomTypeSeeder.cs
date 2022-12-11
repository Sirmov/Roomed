// |-----------------------------------------------------------------------------------------------------|
// <copyright file="RoomTypeSeeder.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Seeding.Seeders
{
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    using Roomed.Data.Models;

    /// <summary>
    /// This is a <see cref="RoomType"/> seeder implementing <see cref="ISeeder"/>.
    /// </summary>
    public class RoomTypeSeeder : ISeeder
    {
        /// <inheritdoc/>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            string json = await File.ReadAllTextAsync("../../Data/Roomed.Data/Seeding/Data/RoomTypeSeed.json");
            var roomTypes = JsonConvert.DeserializeObject<IEnumerable<RoomType>>(json) ?? throw new InvalidOperationException("Deserialization was not successful.");

            foreach (var roomType in roomTypes)
            {
                if (!(await dbContext.RoomTypes.AnyAsync(rt => rt.Name == roomType.Name)))
                {
                    await dbContext.RoomTypes.AddAsync(roomType);
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
