namespace Roomed.Data.Seeding.Seeders
{
    using Newtonsoft.Json;

    using Roomed.Data.Models;

    public class RoomTypeSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            string json = await File.ReadAllTextAsync("../../Data/Roomed.Data/Seeding/Data/RoomTypeSeed.json");
            var roomTypes = JsonConvert.DeserializeObject<IEnumerable<RoomType>>(json);

            if (!dbContext.RoomTypes.Any())
            {
                await dbContext.AddRangeAsync(roomTypes);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
