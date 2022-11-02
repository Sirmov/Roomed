namespace Roomed.Data.Seeding.Seeders
{
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    using Roomed.Data.Models;
    using Roomed.Services.Json.SerializerSettings;

    /// <summary>
    /// This is a <see cref="Profile"/> seeder implementing <see cref="ISeeder"/>.
    /// </summary>
    public class ProfileSeeder : ISeeder
    {
        /// <inheritdoc/>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            string json = await File.ReadAllTextAsync("../../Data/Roomed.Data/Seeding/Data/ProfileSeed.json");

            var profiles = JsonConvert.DeserializeObject<IEnumerable<Profile>>(json, new DateOnlyJsonSettings().Settings);

            foreach (var profile in profiles)
            {
                if (!(await dbContext.Profiles.AnyAsync(p => p.Id == profile.Id)))
                {
                    await dbContext.Profiles.AddAsync(profile);
                }
            }

            await dbContext.SaveChangesAsync();

        }
    }
}
