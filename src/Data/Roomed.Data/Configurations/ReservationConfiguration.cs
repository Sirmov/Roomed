// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationConfiguration.cs" company="Roomed">
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
    /// Model builder configuration for <see cref="Reservation"/> entity.
    /// </summary>
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(r => r.ArrivalDate)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();

            builder.Property(r => r.DepartureDate)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();
        }
    }
}
