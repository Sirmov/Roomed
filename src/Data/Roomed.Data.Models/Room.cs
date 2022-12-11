// |-----------------------------------------------------------------------------------------------------|
// <copyright file="Room.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Data.Common.Models;

    using static Roomed.Common.DataConstants.Room;

    /// <summary>
    /// Room entity model. Inherits <see cref="BaseDeletableModel{TKey}"/>. Has <see cref="int"/> id.
    /// </summary>
    public class Room : BaseDeletableModel<int>
    {
        /// <summary>
        /// Gets or sets room type foreign key.
        /// </summary>
        [Required]
        public int TypeId { get; set; }

        /// <summary>
        /// Gets or sets the number of the room.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(RoomNumberMaxLength)]
        public string Number { get; set; } = null!;

        // Navigational properties

        /// <summary>
        /// Gets or sets room type navigational property.
        /// </summary>
        [ForeignKey(nameof(TypeId))]
        public virtual RoomType Type { get; set; } = null!;
    }
}
