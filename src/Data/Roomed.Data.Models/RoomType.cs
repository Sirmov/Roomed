// |-----------------------------------------------------------------------------------------------------|
// <copyright file="RoomType.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Data.Common.Models;

    using static Roomed.Common.Constants.DataConstants.RoomType;

    /// <summary>
    /// Room type entity model. Inherits <see cref="BaseDeletableModel{TKey}"/>. Has <see cref="int"/> id.
    /// </summary>
    public class RoomType : BaseDeletableModel<int>
    {
        /// <summary>
        /// Gets or sets the name of the type of room.
        /// </summary>
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
