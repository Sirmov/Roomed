namespace Roomed.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Roomed.Data.Common;
    using Roomed.Data.Models;

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
