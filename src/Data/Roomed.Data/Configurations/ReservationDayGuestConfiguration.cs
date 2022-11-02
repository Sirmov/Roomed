namespace Roomed.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Roomed.Data.Models;

    /// <summary>
    /// Model builder configuration for <see cref="ReservationDayGuest"/> entity.
    /// </summary>
    public class ReservationDayGuestConfiguration : IEntityTypeConfiguration<ReservationDayGuest>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ReservationDayGuest> builder)
        {
            builder.HasKey(e => new { e.ReservationDayId, e.GuestId });

            builder.HasOne(e => e.ReservationDay)
                .WithMany(r => r.ReservationDayGuests)
                .HasForeignKey(e => e.ReservationDayId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Guest)
                .WithMany(g => g.GuestReservationDays)
                .HasForeignKey(e => e.GuestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
