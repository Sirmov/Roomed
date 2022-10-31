namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Data.Common.Models;

    using static Roomed.Common.DataConstants.ProfileNote;

    /// <summary>
    /// Profile note entity model. Inherits base deletable model. Has guid id.
    /// </summary>
    public class ProfileNote : BaseDeletableModel<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileNote"/> class with new guid as id.
        /// </summary>
        public ProfileNote()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets profile id foreign key.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public Guid ProfileId { get; set; }

        /// <summary>
        /// Gets or sets profile note body.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ProfileNoteBodyMaxLength)]
        public string Body { get; set; } = null!;

        // Navigational Properties

        /// <summary>
        /// Gets or sets profile navigational property.
        /// </summary>
        [ForeignKey(nameof(ProfileId))]
        public virtual Profile Profile { get; set; } = null!;
    }
}
