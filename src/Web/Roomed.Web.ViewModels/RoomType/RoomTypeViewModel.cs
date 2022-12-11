// |-----------------------------------------------------------------------------------------------------|
// <copyright file="RoomTypeViewModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.ViewModels.RoomType
{
    using Roomed.Services.Data.Dtos.RoomType;
    using Roomed.Services.Mapping;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.RoomType"/> view model.
    /// </summary>
    public class RoomTypeViewModel : IMapFrom<RoomTypeDto>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public int Id { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.RoomType.Name"/>
        public string Name { get; set; } = null!;
    }
}
