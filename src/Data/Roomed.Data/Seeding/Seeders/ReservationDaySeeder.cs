namespace Roomed.Data.Seeding.Seeders
{
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    using Roomed.Data.Models;
    using Roomed.Services.Json.SerializerSettings;

    public class ReservationDaySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            string json = await File.ReadAllTextAsync("../../Data/Roomed.Data/Seeding/Data/ReservationDaySeed.json");
            var reservationDays = JsonConvert.DeserializeObject<IEnumerable<ReservationDay>>(json, new DateOnlyJsonSettings().Settings);

            var reservationDay = dbContext.Reservations
                .Include(r => r.ReservationDays)
                .FirstOrDefault()
                ?.ReservationDays
                .FirstOrDefault();

            if (reservationDay == null)
            {
                await dbContext.AddRangeAsync(reservationDays);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
