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
            var reservationDaysGuests = JsonConvert.DeserializeObject<IEnumerable<ReservationDayGuest>>(json);

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
