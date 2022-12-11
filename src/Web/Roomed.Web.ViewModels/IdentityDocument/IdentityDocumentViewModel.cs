// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IdentityDocumentViewModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.ViewModels.IdentityDocument
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Dtos.IdentityDocument;
    using Roomed.Services.Mapping;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.IdentityDocument"/> view model.
    /// </summary>
    public class IdentityDocumentViewModel : IMapFrom<IdentityDocumentDto>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public Guid Id { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.OwnerId"/>
        public Guid OwnerId { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.Type"/>
        public IdentityDocumentType Type { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.NameInDocument"/>
        [Display(Name = "Name in document")]
        public string NameInDocument { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.DocumentNumber"/>
        [Display(Name = "Document number")]
        public string DocumentNumber { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.PersonalNumber"/>
        [Display(Name = "Personal number")]
        public string? PersonalNumber { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.Country"/>
        public string Country { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.Birthdate"/>
        public DateOnly? Birthdate { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.PlaceOfBirth"/>
        [Display(Name = "Place of birth")]
        public string PlaceOfBirth { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.Nationality"/>
        public string Nationality { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.ValidFrom"/>
        [Display(Name = "Valid from")]
        public DateOnly? ValidFrom { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.ValidUntil"/>
        [Display(Name = "Valid until")]
        public DateOnly? ValidUntil { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.IdentityDocument.IssuedBy"/>
        [Display(Name = "Issued by")]
        public string IssuedBy { get; set; } = null!;
    }
}
