// |-----------------------------------------------------------------------------------------------------|
// <copyright file="GuestsDbContext.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Infrastructure.Guests
{
    using Microsoft.EntityFrameworkCore;

    using Roomed.Data.Configurations;
    using Roomed.Domain.Entities;

    /// <summary>
    /// Entity Framework Core Db context for the guests module.
    /// </summary>
    public class GuestsDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GuestsDbContext"/> class.
        /// </summary>
        public GuestsDbContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GuestsDbContext"/> class.
        /// </summary>
        /// <param name="options">Db context configuring options.</param>
        public GuestsDbContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets db set for <see cref="Profile"/> entity model.
        /// </summary>
        public DbSet<Profile> Profiles { get; set; }

        /// <summary>
        /// Gets or sets db set for <see cref="ProfileNote"/> entity model.
        /// </summary>
        public DbSet<ProfileNote> ProfileNotes { get; set; }

        /// <summary>
        /// Gets or sets db set for <see cref="IdentityDocument"/> entity model.
        /// </summary>
        public DbSet<IdentityDocument> IdentityDocuments { get; set; }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("facility");

            modelBuilder.ApplyConfiguration(new IdentityDocumentConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
