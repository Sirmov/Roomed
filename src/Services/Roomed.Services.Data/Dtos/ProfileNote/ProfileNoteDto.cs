// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ProfileNoteDto.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Dtos.ProfileNote
{
    using Roomed.Services.Data.Dtos.Profile;
    using Roomed.Services.Mapping;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.ProfileNote"/> data transfer object.
    /// </summary>
    public class ProfileNoteDto : IMapFrom<Roomed.Data.Models.ProfileNote>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public Guid Id { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.ProfileNote.Body"/>
        public string Body { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.ProfileNote.ProfileId"/>
        public Guid ProfileId { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.ProfileNote.Profile"/>
        public DetailedProfileDto Profile { get; set; } = null!;
    }
}
