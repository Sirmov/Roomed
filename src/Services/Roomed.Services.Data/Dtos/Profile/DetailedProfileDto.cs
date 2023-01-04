// |-----------------------------------------------------------------------------------------------------|
// <copyright file="DetailedProfileDto.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Dtos.Profile
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Data.Models.Enums;
    using Roomed.Services.Mapping;

    using static Roomed.Common.Constants.DataConstants.Profile;

    /// <summary>
    /// This is a detailed <see cref="Roomed.Data.Models.Profile"/> data transfer object.
    /// </summary>
    public class DetailedProfileDto : ProfileDto, IMapFrom<Roomed.Data.Models.Profile>, IMapTo<Roomed.Data.Models.Profile>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public Guid? Id { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Profile.MiddleName"/>
        [StringLength(MiddleNameMaxLenght, MinimumLength = MiddleNameMinLenght)]
        public string? MiddleName { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Profile.Birthdate"/>
        [Required]
        [DataType(DataType.Date)]
        public DateOnly Birthdate { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Profile.Gender"/>
        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Profile.Nationality"/>
        [Required]
        [StringLength(NationalityMaxLength, MinimumLength = NationalityMinLength)]
        public string Nationality { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.Profile.NationalityCode"/>
        [Required]
        [StringLength(NationalityCodeMaxLength, MinimumLength = NationalityCodeMinLength)]
        public string NationalityCode { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.Profile.Address"/>
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string? Address { get; set; }
    }
}
