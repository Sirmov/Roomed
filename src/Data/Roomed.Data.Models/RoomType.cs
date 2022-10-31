namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Data.Common.Models;

    using static Roomed.Common.DataConstants.RoomType;

    /// <summary>
    /// Room type entity model. Inherits base deletable model. Has int id.
    /// </summary>
    public class RoomType : BaseDeletableModel<int>
    {
        /// <summary>
        /// Gets or sets the name of the type of room.
        /// </summary>
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
