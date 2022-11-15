namespace Roomed.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Roomed.Data.Configurations;
    using Roomed.Data.Models;

    /// <summary>
    /// Application db context using Entity Framework Core, Microsoft SQL Server and ASP Identity. Inherits <see cref="IdentityDbContext{TUser, TRole, TKey}"/>.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        public ApplicationDbContext() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">Db context configuring options.</param>
        public ApplicationDbContext(DbContextOptions options)
            : base(options) { }

        /// <summary>
        /// Gets or sets db set for <see cref="Reservation"/> entity model.
        /// </summary>
        public DbSet<Reservation> Reservations { get; set; } = null!;

        /// <summary>
        /// Gets or sets db set for <see cref="ReservationNote"/> entity model.
        /// </summary>
        public DbSet<ReservationNote> ReservationNotes { get; set; } = null!;

        /// <summary>
        /// Gets or sets db set for <see cref="Profile"/> entity model.
        /// </summary>
        public DbSet<Profile> Profiles { get; set; } = null!;

        /// <summary>
        /// Gets or sets db set for <see cref="ProfileNote"/> entity model.
        /// </summary>
        public DbSet<ProfileNote> ProfileNotes { get; set; } = null!;

        /// <summary>
        /// Gets or sets db set for <see cref="IdentityDocument"/> entity model.
        /// </summary>
        public DbSet<IdentityDocument> IdentityDocuments { get; set; } = null!;

        /// <summary>
        /// Gets or sets db set for <see cref="Room"/> entity model.
        /// </summary>
        public DbSet<Room> Rooms { get; set; } = null!;

        /// <summary>
        /// Gets or sets db set for <see cref="RoomType"/> entity model.
        /// </summary>
        public DbSet<RoomType> RoomTypes { get; set; } = null!;


        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <inheritdoc/>
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
