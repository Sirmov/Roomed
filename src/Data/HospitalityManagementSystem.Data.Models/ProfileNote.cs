namespace HospitalityManagementSystem.Data.Models
{
    using HospitalityManagementSystem.Common;
    using HospitalityManagmentSystem.Data.Common.Models;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ProfileNote : BaseDeletableModel<string>
    {
        public ProfileNote()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string ProfileId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(GlobalConstants.ProfileNoteBodyMaxLength)]
        public string Body { get; set; }

        // Navigational Properties

        [ForeignKey(nameof(ProfileId))]
        public virtual Profile Profile { get; set; }
    }
}
