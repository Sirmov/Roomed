namespace Roomed.Data.Seeding.Seeders
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Newtonsoft.Json;

    using Roomed.Services.Json.SerializerSettings;

    /// <summary>
    /// This is a generic seeder class using the model configuration method.
    /// The seeded data is read from a json file.
    /// </summary>
    /// <typeparam name="TEntity">The model class of the entity.</typeparam>
    public class Seeder<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class
    {
        private readonly string jsonPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="Seeder{TEntity}"/> class.
        /// Sets the directory of the according json file.
        /// </summary>
        /// <param name="jsonPath">The directory of the json file.</param>
        public Seeder(string jsonPath)
        {
            this.jsonPath = jsonPath;
        }

        /// <summary>
        /// This method implements the <see cref="IEntityTypeConfiguration{TEntity}"/> interface.
        /// </summary>
        /// <param name="builder">This is the provided configuration builder.</param>
        public async void Configure(EntityTypeBuilder<TEntity> builder)
        {
            string jsonData = await this.ReadJson();

            IEnumerable<TEntity> data = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(jsonData, new DateOnlyJsonSettings().Settings);

            builder.HasData(data);
        }

        /// <summary>
        /// This method reads the json file asynchronously as string.
        /// </summary>
        /// <returns>This method returns a string representing the json file.</returns>
        /// <exception cref="ArgumentException">This method throws an exception if the provided file does not exist.</exception>
        private async Task<string> ReadJson()
        {
            if (!File.Exists(this.jsonPath))
            {
                throw new ArgumentException($"File {this.jsonPath} can not be found!", nameof(this.jsonPath));
            }

            return await File.ReadAllTextAsync(this.jsonPath);
        }
    }
}
