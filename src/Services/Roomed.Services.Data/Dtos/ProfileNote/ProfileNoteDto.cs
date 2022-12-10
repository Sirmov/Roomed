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

    public class ProfileNoteDto : IMapFrom<Roomed.Data.Models.ProfileNote>
    {
        public Guid Id { get; set; }

        public string Body { get; set; } = null!;

        public Guid ProfileId { get; set; }

        public DetailedProfileDto Profile { get; set; } = null!;
    }
}
