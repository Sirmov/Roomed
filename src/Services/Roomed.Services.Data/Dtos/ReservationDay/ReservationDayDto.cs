// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationDayDto.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Dtos.ReservationDay
{
    using Roomed.Services.Data.Dtos.Reservation;
    using Roomed.Services.Data.Dtos.ReservationDayGuest;
    using Roomed.Services.Data.Dtos.Room;
    using Roomed.Services.Mapping;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.ReservationDay"/> data transfer object.
    /// </summary>
    public class ReservationDayDto : IMapFrom<Roomed.Data.Models.ReservationDay>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public Guid Id { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.ReservationDay.ReservationId"/>
        public Guid ReservationId { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.ReservationDay.RoomId"/>
        public int RoomId { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.ReservationDay.Date"/>
        public DateOnly Date { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.ReservationDay.Reservation"/>
        public ReservationDto Reservation { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.ReservationDay.Room"/>
        public RoomDto Room { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.ReservationDay.ReservationDayGuests"/>
        public ICollection<ReservationDayGuestDto> ReservationDayGuests { get; set; } = new HashSet<ReservationDayGuestDto>();
    }
}
