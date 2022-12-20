// |-----------------------------------------------------------------------------------------------------|
// <copyright file="DetailedProfileInputModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.ViewModels.Profile
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper.Configuration.Annotations;

    using Roomed.Common.Attribues;
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Dtos.Profile;
    using Roomed.Services.Mapping;

    using static Roomed.Common.DataConstants.Profile;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.Profile"/> detailed view model.
    /// </summary>
    public class DetailedProfileInputModel : ProfileInputModel, IMapTo<DetailedProfileDto>, IMapFrom<DetailedProfileDto>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public Guid? Id { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Profile.MiddleName"/>
        [StringLength(MiddleNameMaxLenght, MinimumLength = MiddleNameMinLenght)]
        [Display(Name = "Middle name")]
        [Sanitize]
        public string? MiddleName { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Profile.Birthdate"/>
        [Required]
        [DataType(DataType.Date)]
        public DateOnly? Birthdate { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Profile.Gender"/>
        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Profile.Nationality"/>
        [Required]
        [Sanitize]
        public string Nationality { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.Profile.NationalityCode"/>
        [Required]
        [Sanitize]
        [Display(Name = "Nationality code")]
        public string NationalityCode { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.Profile.Address"/>
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        [Sanitize]
        public string? Address { get; set; }

        /// <summary>
        /// Gets the full name of the guest.
        /// </summary>
        [Ignore]
        public string FullName
        {
            get => $"{this.FirstName} {(this.MiddleName == null ? string.Empty : $"{this.MiddleName} ")}{this.LastName}";
        }
    }
}
