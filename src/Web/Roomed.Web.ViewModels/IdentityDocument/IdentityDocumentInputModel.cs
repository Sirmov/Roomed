// <copyright file="IdentityDocumentInputModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Roomed.Web.ViewModels.IdentityDocument
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Common.Attribues;
    using Roomed.Common.ValidationAttributes;
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Dtos.IdentityDocument;
    using Roomed.Services.Mapping;

    using static Roomed.Common.DataConstants.IdentityDocument;

    public class IdentityDocumentInputModel : IMapFrom<IdentityDocumentDto>, IMapTo<IdentityDocumentDto>
    {
        public Guid? Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public Guid OwnerId { get; set; }

        [Required]
        [EnumDataType(typeof(IdentityDocumentType))]
        public IdentityDocumentType Type { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        [Display(Name = "Name in document")]
        [Sanitize]
        public string NameInDocument { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(NumberMaxLength, MinimumLength = NumberMinLength)]
        [Display(Name = "Document number")]
        [Sanitize]
        public string DocumentNumber { get; set; }

        [StringLength(PersonalNumberMaxLength, MinimumLength = PersonalNumberMinLength)]
        [Display(Name = "Personal number")]
        [Sanitize]
        public string? PersonalNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
        [Sanitize]
        public string Country { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly? Birthdate { get; set; }

        [Required]
        [StringLength(PlaceOfBirthMaxLength, MinimumLength = PlaceOfBirthMinLength)]
        [Display(Name = "Place of birth")]
        [Sanitize]
        public string PlaceOfBirth { get; set; }

        [Required]
        [Sanitize]
        public string Nationality { get; set; }

        [Required]
        [BeforeDate(nameof(ValidUntil))]
        [DataType(DataType.Date)]
        [Display(Name = "Valid from")]
        public DateOnly? ValidFrom { get; set; }

        [Required]
        [AfterDate(nameof(ValidFrom))]
        [DataType(DataType.Date)]
        [Display(Name = "Valid until")]
        public DateOnly? ValidUntil { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(IssuedByMaxLength, MinimumLength = IssuedByMinLength)]
        [Display(Name = "Issued by")]
        [Sanitize]
        public string IssuedBy { get; set; }
    }
}
