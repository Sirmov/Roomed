namespace HospitalityManagementSystem.Data.Configurations
{
    using HospitalityManagementSystem.Data.Common;
    using HospitalityManagementSystem.Data.Models;
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
