namespace Roomed.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Roomed.Data.Common;
    using Roomed.Data.Models;

    /// <summary>
    /// Model builder configuration for <see cref="ReservationDay"/> entity.
    /// </summary>
    public class ReservationDayConfiguration : IEntityTypeConfiguration<ReservationDay>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ReservationDay> builder)
        {
            builder.HasOne(e => e.Reservation)
                .WithMany(r => r.ReservationDays)
                .HasForeignKey(e => e.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(rd => rd.Date)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();
        }
    }
}
