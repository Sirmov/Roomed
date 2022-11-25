namespace Roomed.Services.Data.Dtos.IdentityDocument
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Common.ValidationAttributes;
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Mapping;

    using static Roomed.Common.DataConstants.IdentityDocument;

    public class IdentityDocumentDto : IMapFrom<Roomed.Data.Models.IdentityDocument>, IMapTo<Roomed.Data.Models.IdentityDocument>
    {
        public Guid? Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public Guid OwnerId { get; set; }

        [Required]
        [EnumDataType(typeof(IdentityDocumentType))]
        public IdentityDocumentType Type { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string NameInDocument { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(NumberMaxLength, MinimumLength = NumberMinLength)]
        public string DocumentNumber { get; set; }

        [StringLength(PersonalNumberMaxLength, MinimumLength = PersonalNumberMinLength)]
        public string? PersonalNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
        public string Country { get; set; }

        [Required]
        public DateOnly Birthdate { get; set; }

        [Required]
        [StringLength(PlaceOfBirthMaxLength, MinimumLength = PlaceOfBirthMinLength)]
        public string PlaceOfBirth { get; set; }

        [Required]
        [StringLength(NationalityMaxLength, MinimumLength = NationalityMinLength)]
        public string Nationality { get; set; }

        [Required]
        [BeforeDate(nameof(ValidUntil))]
        public DateOnly ValidFrom { get; set; }

        [Required]
        [AfterDate(nameof(ValidFrom))]
        public DateOnly ValidUntil { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(IssuedByMaxLength, MinimumLength = IssuedByMinLength)]
        public string IssuedBy { get; set; }
    }
}
