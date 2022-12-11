// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationDaySeeder.cs" company="Roomed">
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
    /// This is a <see cref="ReservationDay"/> seeder implementing <see cref="ISeeder"/>.
    /// </summary>
    public class ReservationDaySeeder : ISeeder
    {
        /// <inheritdoc/>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            string json = await File.ReadAllTextAsync("../../Data/Roomed.Data/Seeding/Data/ReservationDaySeed.json");
            var reservationDays = JsonConvert.DeserializeObject<IEnumerable<ReservationDay>>(json, new DateOnlyJsonSettings().Settings)
                ?? throw new InvalidOperationException("Deserialization was not successful.");

            foreach (var reservationDay in reservationDays)
            {
                if (!(await dbContext.Reservations
                    .AnyAsync(r => r.ReservationDays
                        .Any(rd => rd.Id == reservationDay.Id))))
                {
                    await dbContext.AddAsync(reservationDay);
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
