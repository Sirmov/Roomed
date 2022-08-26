namespace HospitalityManagementSystem.Data.Configurations
{
    using HospitalityManagementSystem.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ReservationGuestConfiguration : IEntityTypeConfiguration<ReservationGuest>
    {
        public void Configure(EntityTypeBuilder<ReservationGuest> builder)
        {
            builder.HasKey(e => new { e.ReservationId, e.GuestId });

            builder.HasOne(e => e.Reservation)
                .WithMany(r => r.Guests)
                .HasForeignKey(e => e.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Guest)
                .WithMany(g => g.GuestReservations)
                .HasForeignKey(e => e.GuestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
