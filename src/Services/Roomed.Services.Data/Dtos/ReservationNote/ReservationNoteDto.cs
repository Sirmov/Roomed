// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationNoteDto.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Dtos.ReservationNote
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Services.Data.Dtos.Reservation;
    using Roomed.Services.Mapping;

    using static Roomed.Common.DataConstants.ReservationNote;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.ReservationNote"/> data transfer object.
    /// </summary>
    public class ReservationNoteDto : IMapFrom<Roomed.Data.Models.ReservationNote>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public Guid Id { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.ReservationNote.ReservationId"/>
        public Guid ReservationId { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.ReservationNote.Body"/>
        [StringLength(BodyMaxLength, MinimumLength = BodyMinLength)]
        public string Body { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.ReservationNote.Reservation"/>
        public ReservationDto Reservation { get; set; } = null!;
    }
}
