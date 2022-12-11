// |-----------------------------------------------------------------------------------------------------|
// <copyright file="RoomConfiguration.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Roomed.Data.Models;

    /// <summary>
    /// Model builder configuration for <see cref="Room"/> entity.
    /// </summary>
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder
                .HasIndex(r => r.Number)
                .IsUnique(true);
        }
    }
}
