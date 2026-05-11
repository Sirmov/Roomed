// |-----------------------------------------------------------------------------------------------------|
// <copyright file="FacilityDbContext.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Infrastructure.Facility
{
    using Microsoft.EntityFrameworkCore;
    using Roomed.Data.Configurations;
    using Roomed.Domain.Facility.Entities;

    /// <summary>
    /// Entity Framework Core Db context for the facility module.
    /// </summary>
    public class FacilityDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FacilityDbContext"/> class.
        /// </summary>
        public FacilityDbContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FacilityDbContext"/> class.
        /// </summary>
        /// <param name="options">Db context configuring options.</param>
        public FacilityDbContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets db set for <see cref="Room"/> entity model.
        /// </summary>
        public DbSet<Room> Rooms { get; set; }

        /// <summary>
        /// Gets or sets db set for <see cref="RoomType"/> entity model.
        /// </summary>
        public DbSet<RoomType> RoomTypes { get; set; }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("facility");

            modelBuilder.ApplyConfiguration(new RoomConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
