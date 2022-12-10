// |-----------------------------------------------------------------------------------------------------|
// <copyright file="RoomTypeDto.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Dtos.RoomType
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Services.Mapping;

    using static Roomed.Common.DataConstants.RoomType;

    public class RoomTypeDto : IMapFrom<Roomed.Data.Models.RoomType>
    {
        public int? Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;
    }
}
