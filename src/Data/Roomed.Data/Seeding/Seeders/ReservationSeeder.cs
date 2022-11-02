namespace Roomed.Data.Seeding.Seeders
{
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    using Roomed.Data.Models;
    using Roomed.Services.Json.SerializerSettings;

    public class ReservationSeeder : ISeeder
    {
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
