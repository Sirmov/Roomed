namespace Roomed.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Roomed.Data.Common;
    using Roomed.Data.Models;

    /// <summary>
    /// Model builder configuration for <see cref="IdentityDocument"/> entity.
    /// </summary>
    public class IdentityDocumentConfiguration : IEntityTypeConfiguration<IdentityDocument>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<IdentityDocument> builder)
        {
            builder.Property(e => e.Birthdate)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();

            builder.Property(e => e.ValidFrom)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();

            builder.Property(e => e.ValidUntil)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();
        }
    }
}
