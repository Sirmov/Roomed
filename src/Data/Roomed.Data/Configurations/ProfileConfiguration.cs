// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ProfileConfiguration.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Roomed.Data.Common;
    using Roomed.Data.Models;

    /// <summary>
    /// Model builder configuration for <see cref="Profile"/> entity.
    /// </summary>
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.Property(e => e.Birthdate)
                    .HasConversion<DateOnlyConverter, DateOnlyComparer>();
        }
    }
}
