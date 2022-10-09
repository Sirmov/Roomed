namespace Roomed.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Roomed.Data.Models;

    public class ReservationDayGuestConfiguration : IEntityTypeConfiguration<ReservationDayGuest>
    {
        public void Configure(EntityTypeBuilder<ReservationDayGuest> builder)
        {
            builder.HasKey(e => new { e.ReservationDayId, e.GuestId });

            builder.HasOne(e => e.ReservationDay)
                .WithMany(r => r.Guests)
                .HasForeignKey(e => e.ReservationDayId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Guest)
                .WithMany(g => g.GuestReservationDays)
                .HasForeignKey(e => e.GuestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
