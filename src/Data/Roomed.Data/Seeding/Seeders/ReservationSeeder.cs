// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationSeeder.cs" company="Roomed">
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
    /// This is a <see cref="Reservation"/> seeder implementing <see cref="ISeeder"/>.
    /// </summary>
    public class ReservationSeeder : ISeeder
    {
        /// <inheritdoc/>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            string json = await File.ReadAllTextAsync("../../Data/Roomed.Data/Seeding/Data/ReservationSeed.json");
            var reservations = JsonConvert.DeserializeObject<IEnumerable<Reservation>>(json, new DateOnlyJsonSettings().Settings);

            foreach (var reservation in reservations)
            {
                if (!(await dbContext.Reservations.AnyAsync(r => r.Id == reservation.Id)))
                {
                    await dbContext.AddAsync(reservation);
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
