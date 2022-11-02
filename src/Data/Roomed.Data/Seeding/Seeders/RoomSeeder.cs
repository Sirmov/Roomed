namespace Roomed.Data.Seeding.Seeders
{
    using Newtonsoft.Json;

    using Roomed.Data.Models;

    public class RoomSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            string json = await File.ReadAllTextAsync("../../Data/Roomed.Data/Seeding/Data/RoomSeed.json");
            var rooms = JsonConvert.DeserializeObject<IEnumerable<Room>>(json);

            if (!dbContext.Rooms.Any())
            {
                int doubleRoomSeaViewId = dbContext.RoomTypes.Where(rt => rt.Name == "Double room sea view").First().Id;
                int doubleRoomParkViewId = dbContext.RoomTypes.Where(rt => rt.Name == "Double room park view").First().Id;

                foreach (var room in rooms)
                {
                    if (int.Parse(room.Number) % 2 == 0)
                    {
                        room.TypeId = doubleRoomParkViewId;
                    }
                    else
                    {
                        room.TypeId = doubleRoomSeaViewId;
                    }
                }

                await dbContext.AddRangeAsync(rooms);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
