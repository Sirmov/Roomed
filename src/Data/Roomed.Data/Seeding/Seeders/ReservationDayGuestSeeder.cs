namespace Roomed.Data.Seeding.Seeders
{
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Roomed.Data.Models;

    public class ReservationDayGuestSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            string json = await File.ReadAllTextAsync("../../Data/Roomed.Data/Seeding/Data/ReservationDayGuestSeed.json");
            var reservationDaysGuests = JsonConvert.DeserializeObject<IEnumerable<ReservationDayGuest>>(json);

            var reservation = dbContext.Reservations
                .Include(r => r.ReservationDays)
                .ThenInclude(rd => rd.ReservationDayGuests)
                .FirstOrDefault();
            var reservationDay = reservation?.ReservationDays.FirstOrDefault();
            var reservationDayGuest = reservationDay?.ReservationDayGuests.FirstOrDefault();

            if (reservationDayGuest == null)
            {
                await dbContext.AddRangeAsync(reservationDaysGuests);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
