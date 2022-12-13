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

    public static class DbContextMock
    {
        public static ApplicationDbContext DbContext { get; set; }

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

            await dbContext.Database.EnsureCreatedAsync();

            DbContext = dbContext;
            return dbContext;
        }

        public static async Task DisposeAsync()
        {
            await DbContext.Database.EnsureDeletedAsync();
            await DbContext.DisposeAsync();
        }
    }
}
