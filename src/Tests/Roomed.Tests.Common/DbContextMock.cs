// |-----------------------------------------------------------------------------------------------------|
// <copyright file="DbContextMock.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Tests.Common
{
    using Microsoft.EntityFrameworkCore;
    using Roomed.Data;

    /// <summary>
    /// This class is a mock of <see cref="ApplicationDbContext"/>.
    /// </summary>
    public static class DbContextMock
    {
        /// <summary>
        /// Gets or sets the instance of <see cref="ApplicationDbContext"/>.
        /// </summary>
        public static ApplicationDbContext DbContext { get; set; } = null!;

        /// <summary>
        /// This method asynchronously initializes the db context.
        /// </summary>
        /// <returns>Returns a <see cref="Task{TResult}"/> with <see cref="ApplicationDbContext"/>.</returns>
        public static async Task<ApplicationDbContext> InitializeDbContextAsync()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("RoomedInMemory")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            if (dbContext != null)
            {
                await dbContext.Database.EnsureDeletedAsync();
            }

            DbContext = dbContext;
            return dbContext;
        }

        /// <summary>
        /// This method asynchronously deletes and disposes the database.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        public static async Task DisposeAsync()
        {
            await DbContext.Database.EnsureDeletedAsync();
            await DbContext.DisposeAsync();
        }
    }
}
