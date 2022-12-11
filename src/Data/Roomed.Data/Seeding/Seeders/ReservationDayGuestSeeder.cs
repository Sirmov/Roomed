// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationDayGuestSeeder.cs" company="Roomed">
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
    /// This is a <see cref="ReservationDayGuest"/> seeder implementing <see cref="ISeeder"/>.
    /// </summary>
    public class ReservationDayGuestSeeder : ISeeder
    {
        /// <inheritdoc/>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            string json = await File.ReadAllTextAsync("../../Data/Roomed.Data/Seeding/Data/ReservationDayGuestSeed.json");
            var reservationDaysGuests = JsonConvert.DeserializeObject<IEnumerable<ReservationDayGuest>>(json) ?? throw new InvalidOperationException("Deserialization was not successful.");

            foreach (var reservationDayGuest in reservationDaysGuests)
            {
                if (!(await dbContext.Reservations
                    .AnyAsync(r => r.ReservationDays
                        .Any(rd => rd.ReservationDayGuests
                            .Any(rdg => rdg.Id == reservationDayGuest.Id)))))
                {
                    await dbContext.AddAsync(reservationDayGuest);
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
