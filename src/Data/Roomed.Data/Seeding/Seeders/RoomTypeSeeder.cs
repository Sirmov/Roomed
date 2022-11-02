namespace Roomed.Data.Seeding.Seeders
{
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    using Roomed.Data.Models;

    public class RoomTypeSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            string json = await File.ReadAllTextAsync("../../Data/Roomed.Data/Seeding/Data/RoomTypeSeed.json");
            var roomTypes = JsonConvert.DeserializeObject<IEnumerable<RoomType>>(json);

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
