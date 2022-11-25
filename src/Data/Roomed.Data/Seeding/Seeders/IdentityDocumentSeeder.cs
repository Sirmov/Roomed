namespace Roomed.Data.Seeding.Seeders
{
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    using Roomed.Data.Models;
    using Roomed.Services.Json.SerializerSettings;

    /// <summary>
    /// This is a <see cref="IdentityDocument"/> seeder implementing <see cref="ISeeder"/>.
    /// </summary>
    public class IdentityDocumentSeeder : ISeeder
    {
        /// <inheritdoc/>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            string json = await File.ReadAllTextAsync("../../Data/Roomed.Data/Seeding/Data/IdentityDocumentSeed.json");
            var identityDocuments = JsonConvert.DeserializeObject<IEnumerable<IdentityDocument>>(json, new DateOnlyJsonSettings().Settings);

            foreach (var identityDocument in identityDocuments)
            {
                if (!(await dbContext.IdentityDocuments.AnyAsync(id => id.Id == identityDocument.Id)))
                {
                    await dbContext.IdentityDocuments.AddAsync(identityDocument);
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
