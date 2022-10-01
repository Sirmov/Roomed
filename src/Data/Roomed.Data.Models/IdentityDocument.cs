namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Common;
    using Roomed.Data.Models.Enums;
    using Roomed.Data.Common.Models;

    public class IdentityDocument : BaseDeletableModel<string>
    {
        public IdentityDocument()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required(AllowEmptyStrings = false)]
        public string OwnerId { get; set; }

        [Required]
        [EnumDataType(typeof(IdentityDocumentType))]
        public IdentityDocumentType Type { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(GlobalConstants.IdentityDocumentNameMaxLength)]
        public string NameInDocument { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(GlobalConstants.IdentityDocumentNumberMaxLength)]
        public string DocumentNumber { get; set; }

        [MaxLength(GlobalConstants.IdentityDocumentPersonalNumberMaxLength)]
        public string? PersonalNumber { get; set; }

        [MaxLength(GlobalConstants.IdentityDocumentCountryMaxLength)]
        public string Country { get; set; }

        [Required]
        public DateOnly Birthdate { get; set; }

        [Required]
        [MaxLength(GlobalConstants.IdentityDocumentPlaceOfBirthMaxLength)]
        public string PlaceOfBirth { get; set; }

        [MaxLength(GlobalConstants.IdentityDocumentNationalityMaxLength)]
        public string Nationality { get; set; }

        [Required]
        public DateOnly ValidFrom { get; set; }

        [Required]
        public DateOnly ValidUntil { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(GlobalConstants.IdentityDocumentIssuedByMaxLength)]
        public string IssuedBy { get; set; }

        // Navigational Properties

        [ForeignKey(nameof(OwnerId))]
        public virtual Profile Owner { get; set; }
    }
}
