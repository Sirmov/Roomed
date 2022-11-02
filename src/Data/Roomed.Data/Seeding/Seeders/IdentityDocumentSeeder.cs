namespace Roomed.Data.Seeding.Seeders
{
    using Newtonsoft.Json;

    using Roomed.Data.Models;
    using Roomed.Services.Json.SerializerSettings;

    public class IdentityDocumentSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            string json = await File.ReadAllTextAsync("../../Data/Roomed.Data/Seeding/Data/IdentityDocumentSeed.json");
            var identityDocuments = JsonConvert.DeserializeObject<IEnumerable<IdentityDocument>>(json, new DateOnlyJsonSettings().Settings);

            if (!dbContext.IdentityDocuments.Any())
            {
                await dbContext.AddRangeAsync(identityDocuments);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
