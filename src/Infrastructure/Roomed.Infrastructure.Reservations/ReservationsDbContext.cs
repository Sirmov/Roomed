// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationsDbContext.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Infrastructure.Reservations
{
    using Microsoft.EntityFrameworkCore;

    using Roomed.Data.Configurations;
    using Roomed.Domain.Reservations.Entities;

    /// <summary>
    /// Entity Framework Core Db context for the reservations module.
    /// </summary>
    public class ReservationsDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationsDbContext"/> class.
        /// </summary>
        public ReservationsDbContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationsDbContext"/> class.
        /// </summary>
        /// <param name="options">Db context configuring options.</param>
        public ReservationsDbContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets db set for <see cref="Reservation"/> entity model.
        /// </summary>
        public DbSet<Reservation> Reservations { get; set; }

        /// <summary>
        /// Gets or sets db set for <see cref="ReservationNote"/> entity model.
        /// </summary>
        public DbSet<ReservationNote> ReservationNotes { get; set; }

        /// <summary>
        /// Gets or sets db set for <see cref="ReservationDay"/> entity model.
        /// </summary>
        public DbSet<ReservationDay> ReservationDays { get; set; }

        /// <summary>
        /// Gets or sets db set for <see cref="ReservationDayGuest"/> entity model.
        /// </summary>
        public DbSet<ReservationDayGuest> ReservationsDaysGuests { get; set; }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("reservation");

            modelBuilder.ApplyConfiguration(new ReservationDayConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationDayGuestConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
