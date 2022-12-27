// |-----------------------------------------------------------------------------------------------------|
// <copyright file="RoomDto.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Dtos.Room
{
    using Roomed.Services.Data.Dtos.RoomType;
    using Roomed.Services.Mapping;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.Room"/> data transfer object.
    /// </summary>
    public class RoomDto : IMapFrom<Roomed.Data.Models.Room>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public int Id { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Room.TypeId"/>
        public int TypeId { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Room.Number"/>
        public string Number { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.Room.Type"/>
        public RoomTypeDto Type { get; set; } = null!;
    }
}
