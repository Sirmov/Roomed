// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IdentityDocumentConfiguration.cs" company="Roomed">
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

            builder.HasIndex(e => e.DocumentNumber)
                .IsUnique(true);
        }
    }
}
