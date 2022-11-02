namespace Roomed.Data.Seeding.Seeders
{
    using Newtonsoft.Json;

    using Roomed.Data.Models;
    using Roomed.Services.Json.SerializerSettings;

    public class ProfileSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            string json = await File.ReadAllTextAsync("../../Data/Roomed.Data/Seeding/Data/ProfileSeed.json");

            var profiles = JsonConvert.DeserializeObject<IEnumerable<Profile>>(json, new DateOnlyJsonSettings().Settings);

            if (!dbContext.Profiles.Any())
            {
                await dbContext.AddRangeAsync(profiles);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
