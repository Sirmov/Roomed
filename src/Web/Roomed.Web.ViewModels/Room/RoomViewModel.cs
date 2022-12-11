// |-----------------------------------------------------------------------------------------------------|
// <copyright file="RoomViewModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.ViewModels.Room
{
    using Roomed.Services.Data.Dtos.Room;
    using Roomed.Services.Mapping;
    using Roomed.Web.ViewModels.RoomType;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.Room"/> view model.
    /// </summary>
    public class RoomViewModel : IMapFrom<RoomDto>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public int Id { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Room.TypeId"/>
        public int TypeId { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Room.Number"/>
        public string Number { get; set; } = null!;

        /// <summary>
        /// Gets or sets the room type view model.
        /// </summary>
        public RoomTypeViewModel Type { get; set; } = null!;
    }
}
