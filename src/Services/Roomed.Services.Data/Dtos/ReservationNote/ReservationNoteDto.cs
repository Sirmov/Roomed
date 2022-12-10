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

    public class ReservationNoteDto : IMapFrom<Roomed.Data.Models.ReservationNote>
    {
        public Guid Id { get; set; }

        public Guid ReservationId { get; set; }

        [StringLength(BodyMaxLength, MinimumLength = BodyMinLength)]
        public string Body { get; set; } = null!;

        public virtual ReservationDto Reservation { get; set; } = null!;
    }
}
