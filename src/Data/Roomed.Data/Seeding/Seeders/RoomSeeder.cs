// |-----------------------------------------------------------------------------------------------------|
// <copyright file="RoomSeeder.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Seeding.Seeders
{
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    using Roomed.Common.Constants;
    using Roomed.Data.Models;

    /// <summary>
    /// This is a <see cref="Room"/> seeder implementing <see cref="ISeeder"/>.
    /// </summary>
    public class RoomSeeder : ISeeder
    {
        /// <inheritdoc/>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            string json = await File.ReadAllTextAsync("../../Data/Roomed.Data/Seeding/Data/RoomSeed.json");
            var rooms = JsonConvert.DeserializeObject<IEnumerable<Room>>(json)
                ?? throw new InvalidOperationException(ErrorMessagesConstants.DeserializationFailed);

            int doubleRoomSeaViewId = dbContext.RoomTypes.Where(rt => rt.Name == "Double room sea view").First().Id;
            int doubleRoomParkViewId = dbContext.RoomTypes.Where(rt => rt.Name == "Double room park view").First().Id;

            foreach (var room in rooms)
            {
                if (!(await dbContext.Rooms.AnyAsync(r => r.Number == room.Number)))
                {
                    if (int.Parse(room.Number) % 2 == 0)
                    {
                        room.TypeId = doubleRoomParkViewId;
                    }
                    else
                    {
                        room.TypeId = doubleRoomSeaViewId;
                    }

                    await dbContext.Rooms.AddAsync(room);
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
