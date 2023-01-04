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

    using static Roomed.Common.Constants.DataConstants.RoomType;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.RoomType"/> data transfer object.
    /// </summary>
    public class RoomTypeDto : IMapFrom<Roomed.Data.Models.RoomType>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public int? Id { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.RoomType.Name"/>
        [Required(AllowEmptyStrings = false)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;
    }
}
