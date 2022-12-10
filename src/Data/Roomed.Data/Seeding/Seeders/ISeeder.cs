// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ISeeder.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Seeding.Seeders
{
    /// <summary>
    /// This interface documents the functionality needed to create a custom seeder using the db context method.
    /// </summary>
    public interface ISeeder
    {
        /// <summary>
        /// This method is called to seed the database. The method checks if the data is seeded and only if it is not, writes it to the database.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="serviceProvider">The IoC container, service provider.</param>
        /// <returns>Returns <see cref="Task"/>.</returns>
        Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider);
    }
}
