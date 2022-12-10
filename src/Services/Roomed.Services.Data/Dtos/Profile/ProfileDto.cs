// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ProfileDto.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Dtos.Profile
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Services.Mapping;

    using static Roomed.Common.DataConstants.Profile;

    public class ProfileDto : IMapFrom<Roomed.Data.Models.Profile>
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; } = null!;
    }
}
