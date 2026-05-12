// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ModelConfigurationSeeder.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Seeding.Seeders
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Newtonsoft.Json;

    using Roomed.Common.Constants;
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
        /// Initializes a new instance of the <see cref="ModelConfigurationSeeder{TEntity}"/> class.
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

            IEnumerable<TEntity>? data = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(jsonData, new DateOnlyJsonSettings().Settings);

            if (data == null)
            {
                throw new NullReferenceException(string.Format(ErrorMessagesConstants.NullReference, "Deserialized object"));
            }

            builder.HasData(data);
        }
    }
}
