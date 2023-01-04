// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IdentityDocumentInputModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.ViewModels.IdentityDocument
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Common.Attribues;
    using Roomed.Common.ValidationAttributes;
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Dtos.IdentityDocument;
    using Roomed.Services.Mapping;

    using static Roomed.Common.Constants.DataConstants.IdentityDocument;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.IdentityDocument"/> input model.
    /// </summary>
    public class IdentityDocumentInputModel : IMapFrom<IdentityDocumentDto>, IMapTo<IdentityDocumentDto>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public Guid? Id { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.OwnerId"/>
        [Required(AllowEmptyStrings = false)]
        public Guid OwnerId { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.Type"/>
        [Required]
        [EnumDataType(typeof(IdentityDocumentType))]
        public IdentityDocumentType Type { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.NameInDocument"/>
        [Required(AllowEmptyStrings = false)]
        [Sanitize]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        [Display(Name = "Name in document")]
        public string NameInDocument { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.DocumentNumber"/>
        [Required(AllowEmptyStrings = false)]
        [Sanitize]
        [StringLength(NumberMaxLength, MinimumLength = NumberMinLength)]
        [Display(Name = "Document number")]
        public string DocumentNumber { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.PersonalNumber"/>
        [Sanitize]
        [StringLength(PersonalNumberMaxLength, MinimumLength = PersonalNumberMinLength)]
        [Display(Name = "Personal number")]
        public string? PersonalNumber { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.Country"/>
        [Required(AllowEmptyStrings = false)]
        [Sanitize]
        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
        public string Country { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.Birthdate"/>
        [Required]
        [DataType(DataType.Date)]
        public DateOnly? Birthdate { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.PlaceOfBirth"/>
        [Required]
        [Sanitize]
        [StringLength(PlaceOfBirthMaxLength, MinimumLength = PlaceOfBirthMinLength)]
        [Display(Name = "Place of birth")]
        public string PlaceOfBirth { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.Nationality"/>
        [Required]
        [Sanitize]
        public string Nationality { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.ValidFrom"/>
        [Required]
        [BeforeDate(typeof(DateOnly), nameof(ValidUntil))]
        [DataType(DataType.Date)]
        [Display(Name = "Valid from")]
        public DateOnly? ValidFrom { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.ValidUntil"/>
        [Required]
        [AfterDate(typeof(DateOnly), nameof(ValidFrom))]
        [DataType(DataType.Date)]
        [Display(Name = "Valid until")]
        public DateOnly? ValidUntil { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.IssuedBy"/>
        [Required(AllowEmptyStrings = false)]
        [Sanitize]
        [StringLength(IssuedByMaxLength, MinimumLength = IssuedByMinLength)]
        [Display(Name = "Issued by")]
        public string IssuedBy { get; set; } = null!;
    }
}
