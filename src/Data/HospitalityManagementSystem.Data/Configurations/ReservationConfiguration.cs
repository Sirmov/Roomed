namespace HospitalityManagementSystem.Data.Configurations
{
    using HospitalityManagementSystem.Data.Common;
    using HospitalityManagementSystem.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(r => r.ArrivalDate)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();

            builder.Property(r => r.DepartureDate)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();
        }
    }
}
