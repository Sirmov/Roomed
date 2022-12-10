// <copyright file="IdentityDocumentViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }

        public IdentityDocumentType Type { get; set; }

        [Display(Name = "Name in document")]
        public string NameInDocument { get; set; }

        [Display(Name = "Document number")]
        public string DocumentNumber { get; set; }

        [Display(Name = "Personal number")]
        public string? PersonalNumber { get; set; }

        public string Country { get; set; }

        public DateOnly? Birthdate { get; set; }

        [Display(Name = "Place of birth")]
        public string PlaceOfBirth { get; set; }

        public string Nationality { get; set; }

        [Display(Name = "Valid from")]
        public DateOnly? ValidFrom { get; set; }

        [Display(Name = "Valid until")]
        public DateOnly? ValidUntil { get; set; }

        [Display(Name = "Issued by")]
        public string IssuedBy { get; set; }
    }
}
