namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Data.Common.Models;

    using static Roomed.Common.DataConstants.Room;

    /// <summary>
    /// Room entity model. Inherits base deletable model. Has int id.
    /// </summary>
    public class Room : BaseDeletableModel<int>
    {
        /// <summary>
        /// Gets or sets room type foreign key.
        /// </summary>
        [Required]
        public int TypeId { get; set; }

        /// <summary>
        /// Gets or sets the number of the room.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(RoomNumberMaxLength)]
        public string Number { get; set; } = null!;

        // Navigational properties

        /// <summary>
        /// Gets or sets room type navigational property.
        /// </summary>
        [ForeignKey(nameof(TypeId))]
        public virtual RoomType Type { get; set; } = null!;
    }
}
