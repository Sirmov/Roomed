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
    public class ModelConfigurationSeeder<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class
    {
        private readonly string jsonPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="Seeder{TEntity}"/> class.
        /// Sets the directory of the according json file.
        /// </summary>
        /// <param name="jsonPath">The directory of the json file.</param>
        public ModelConfigurationSeeder(string jsonPath)
        {
            this.jsonPath = jsonPath;
        }

        /// <summary>
        /// This method implements the <see cref="IEntityTypeConfiguration{TEntity}"/> interface.
        /// </summary>
        /// <param name="builder">This is the provided configuration builder.</param>
        public async void Configure(EntityTypeBuilder<TEntity> builder)
        {
            string jsonData = await File.ReadAllTextAsync(this.jsonPath);

            IEnumerable<TEntity> data = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(jsonData, new DateOnlyJsonSettings().Settings);

            builder.HasData(data);
        }
    }
}
