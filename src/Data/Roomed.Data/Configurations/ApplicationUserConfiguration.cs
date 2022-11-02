﻿namespace Roomed.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Roomed.Data.Models;

    using static Roomed.Common.DataConstants.ApplicationUser;

    /// <summary>
    /// Model builder configuration for <see cref="ApplicationUser"/> entity.
    /// </summary>
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(au => au.UserName)
                .HasMaxLength(UserNameMaxLength)
                .IsRequired(true);

            builder.Property(au => au.Email)
                    .HasMaxLength(EmailMaxLength)
                    .IsRequired(true);

            builder.HasIndex(au => au.UserName)
                    .IsUnique();

            builder.HasIndex(au => au.Email)
                    .IsUnique();
        }
    }
}
