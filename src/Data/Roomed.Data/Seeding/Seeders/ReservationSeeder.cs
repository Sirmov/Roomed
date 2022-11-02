namespace Roomed.Data.Seeding.Seeders
{
    using Newtonsoft.Json;

    using Roomed.Data.Models;
    using Roomed.Services.Json.SerializerSettings;

    public class ReservationSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            string json = await File.ReadAllTextAsync("../../Data/Roomed.Data/Seeding/Data/ReservationSeed.json");
            var reservations = JsonConvert.DeserializeObject<IEnumerable<Reservation>>(json, new DateOnlyJsonSettings().Settings);

            if (!dbContext.Reservations.Any())
            {
                await dbContext.AddRangeAsync(reservations);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
