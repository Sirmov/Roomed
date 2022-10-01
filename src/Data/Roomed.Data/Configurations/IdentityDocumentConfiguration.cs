namespace Roomed.Data.Configurations
{
    using Roomed.Data.Common;
    using Roomed.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class IdentityDocumentConfiguration : IEntityTypeConfiguration<IdentityDocument>
    {
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
