﻿namespace Roomed.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Roomed.Data.Configurations;
    using Roomed.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<ReservationNote> ReservationNotes { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<ProfileNote> ProfileNotes { get; set; }

        public DbSet<IdentityDocument> IdentityDocuments { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomType> RoomTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(@"Server=localhost;Database=Roomed;User Id=sa;Password=123Nikola321!;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Applying configuration
            modelBuilder.ApplyConfiguration<ApplicationUser>(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration<Reservation>(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration<IdentityDocument>(new IdentityDocumentConfiguration());
            modelBuilder.ApplyConfiguration<Profile>(new ProfileConfiguration());
            modelBuilder.ApplyConfiguration<ReservationDay>(new ReservationDayConfiguration());
            modelBuilder.ApplyConfiguration<ReservationDayGuest>(new ReservationDayGuestConfiguration());
            modelBuilder.ApplyConfiguration<Room>(new RoomConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
