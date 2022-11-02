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
