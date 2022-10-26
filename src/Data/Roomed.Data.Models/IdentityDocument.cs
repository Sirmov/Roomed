namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Data.Common.Models;
    using Roomed.Data.Models.Enums;

    using static Roomed.Common.DataConstants.IdentityDocument;

    public class IdentityDocument : BaseDeletableModel<Guid>
    {
        public IdentityDocument()
        {
            this.Id = Guid.NewGuid();
        }

        [Required(AllowEmptyStrings = false)]
        public Guid OwnerId { get; set; }

        [Required]
        [EnumDataType(typeof(IdentityDocumentType))]
        public IdentityDocumentType Type { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(IdentityDocumentNameMaxLength)]
        public string NameInDocument { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(IdentityDocumentNumberMaxLength)]
        public string DocumentNumber { get; set; }

        [MaxLength(IdentityDocumentPersonalNumberMaxLength)]
        public string? PersonalNumber { get; set; }

        [MaxLength(IdentityDocumentCountryMaxLength)]
        public string Country { get; set; }

        [Required]
        public DateOnly Birthdate { get; set; }

        [Required]
        [MaxLength(IdentityDocumentPlaceOfBirthMaxLength)]
        public string PlaceOfBirth { get; set; }

        [MaxLength(IdentityDocumentNationalityMaxLength)]
        public string Nationality { get; set; }

        [Required]
        public DateOnly ValidFrom { get; set; }

        [Required]
        public DateOnly ValidUntil { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(IdentityDocumentIssuedByMaxLength)]
        public string IssuedBy { get; set; }

        // Navigational Properties
        [ForeignKey(nameof(OwnerId))]
        public virtual Profile Owner { get; set; }
    }
}
