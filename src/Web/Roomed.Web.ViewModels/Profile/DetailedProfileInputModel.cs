// <copyright file="DetailedProfileInputModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Roomed.Web.ViewModels.Profile
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper.Configuration.Annotations;

    using Roomed.Common.Attribues;
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Dtos.Profile;
    using Roomed.Services.Mapping;

    using static Roomed.Common.DataConstants.Profile;

    public class DetailedProfileInputModel : IMapTo<DetailedProfileDto>, IMapFrom<DetailedProfileDto>
    {
        public Guid? Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        [Display(Name = "First name")]
        [Sanitize]
        public string FirstName { get; set; } = null!;

        [StringLength(MiddleNameMaxLenght, MinimumLength = MiddleNameMinLenght)]
        [Display(Name = "Middle name")]
        [Sanitize]
        public string? MiddleName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        [Display(Name = "Last name")]
        [Sanitize]
        public string LastName { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateOnly? Birthdate { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Required]
        [Sanitize]
        public string Nationality { get; set; }

        [Required]
        [Sanitize]
        [Display(Name = "Nationality code")]
        public string NationalityCode { get; set; }

        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        [Sanitize]
        public string? Address { get; set; }

        [Ignore]
        public string FullName
        {
            get => $"{this.FirstName} {(this.MiddleName == null ? string.Empty : $"{this.MiddleName} ")}{this.LastName}";
        }
    }
}
