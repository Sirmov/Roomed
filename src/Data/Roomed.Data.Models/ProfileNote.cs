namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Data.Common.Models;

    using static Roomed.Common.DataConstants.ProfileNote;

    public class ProfileNote : BaseDeletableModel<Guid>
    {
        public ProfileNote()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid ProfileId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ProfileNoteBodyMaxLength)]
        public string Body { get; set; }

        // Navigational Properties
        [ForeignKey(nameof(ProfileId))]
        public virtual Profile Profile { get; set; }
    }
}
