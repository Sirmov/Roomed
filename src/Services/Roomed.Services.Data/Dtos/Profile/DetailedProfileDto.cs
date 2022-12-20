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

    using static Roomed.Common.DataConstants.Profile;

    public class DetailedProfileDto : ProfileDto, IMapFrom<Roomed.Data.Models.Profile>, IMapTo<Roomed.Data.Models.Profile>
    {
        public Guid? Id { get; set; }

        [StringLength(MiddleNameMaxLenght, MinimumLength = MiddleNameMinLenght)]
        public string? MiddleName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly Birthdate { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Required]
        [StringLength(NationalityMaxLength, MinimumLength = NationalityMinLength)]
        public string Nationality { get; set; }

        [Required]
        [StringLength(NationalityCodeMaxLength, MinimumLength = NationalityCodeMinLength)]
        public string NationalityCode { get; set; }

        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string? Address { get; set; }
    }
}
