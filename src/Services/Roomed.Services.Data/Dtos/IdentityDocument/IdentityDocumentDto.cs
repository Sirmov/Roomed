// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IdentityDocumentDto.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Dtos.IdentityDocument
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Common.ValidationAttributes;
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Mapping;

    using static Roomed.Common.DataConstants.IdentityDocument;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.IdentityDocument"/> data transfer object.
    /// </summary>
    public class IdentityDocumentDto : IMapFrom<Roomed.Data.Models.IdentityDocument>, IMapTo<Roomed.Data.Models.IdentityDocument>
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
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string NameInDocument { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.DocumentNumber"/>
        [Required(AllowEmptyStrings = false)]
        [StringLength(NumberMaxLength, MinimumLength = NumberMinLength)]
        public string DocumentNumber { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.PersonalNumber"/>
        [StringLength(PersonalNumberMaxLength, MinimumLength = PersonalNumberMinLength)]
        public string? PersonalNumber { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.Country"/>
        [Required(AllowEmptyStrings = false)]
        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
        public string Country { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.Birthdate"/>
        [Required]
        public DateOnly Birthdate { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.PlaceOfBirth"/>
        [Required]
        [StringLength(PlaceOfBirthMaxLength, MinimumLength = PlaceOfBirthMinLength)]
        public string PlaceOfBirth { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.Nationality"/>
        [Required]
        [StringLength(NationalityMaxLength, MinimumLength = NationalityMinLength)]
        public string Nationality { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.ValidFrom"/>
        [Required]
        [BeforeDate(nameof(ValidUntil))]
        public DateOnly ValidFrom { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.ValidUntil"/>
        [Required]
        [AfterDate(nameof(ValidFrom))]
        public DateOnly ValidUntil { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.IssuedBy"/>
        [Required(AllowEmptyStrings = false)]
        [StringLength(IssuedByMaxLength, MinimumLength = IssuedByMinLength)]
        public string IssuedBy { get; set; } = null!;
    }
}
