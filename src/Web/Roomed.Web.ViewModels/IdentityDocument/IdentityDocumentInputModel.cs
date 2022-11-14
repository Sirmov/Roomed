namespace Roomed.Web.ViewModels.IdentityDocument
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Roomed.Common.ValidationAttributes;
    using Roomed.Data.Models.Enums;

    using static Roomed.Common.DataConstants.IdentityDocument;

    public class IdentityDocumentInputModel
    {
        [Required(AllowEmptyStrings = false)]
        public Guid OwnerId { get; set; }

        [Required]
        [EnumDataType(typeof(IdentityDocumentType))]
        public IdentityDocumentType Type { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        [Display(Name = "Name in document")]
        public string NameInDocument { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(NumberMaxLength, MinimumLength = NumberMinLength)]
        [Display(Name = "Document Number")]
        public string DocumentNumber { get; set; }

        [StringLength(PersonalNumberMaxLength, MinimumLength = PersonalNumberMinLength)]
        [Display(Name = "Personal number")]
        public string? PersonalNumber { get; set; }

        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
        public string Country { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly Birthdate { get; set; }

        [Required]
        [StringLength(PlaceOfBirthMaxLength, MinimumLength = PlaceOfBirthMinLength)]
        [Display(Name = "Place of birth")]
        public string PlaceOfBirth { get; set; }

        [Required]
        [StringLength(NationalityMaxLength, MinimumLength = NationalityMinLength)]
        public string Nationality { get; set; }

        [Required]
        [BeforeDate(nameof(ValidUntil))]
        [DataType(DataType.Date)]
        [Display(Name = "Valid from")]
        public DateOnly ValidFrom { get; set; }

        [Required]
        [AfterDate(nameof(ValidFrom))]
        [DataType(DataType.Date)]
        [Display(Name = "Valid until")]
        public DateOnly ValidUntil { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(IssuedByMaxLength, MinimumLength = IssuedByMinLength)]
        [Display(Name = "Issued by")]
        public string IssuedBy { get; set; }
    }
}
