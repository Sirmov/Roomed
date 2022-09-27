namespace Roomed.Data.Configurations
{
    using Roomed.Data.Common;
    using Roomed.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.Property(e => e.Birthdate)
                    .HasConversion<DateOnlyConverter, DateOnlyComparer>();
        }
    }
}
