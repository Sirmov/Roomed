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

    public class ReservationDayDto : IMapFrom<Roomed.Data.Models.ReservationDay>
    {
        public Guid Id { get; set; }

        public Guid ReservationId { get; set; }

        public int RoomId { get; set; }

        public DateOnly Date { get; set; }

        public ReservationDto Reservation { get; set; } = null!;

        public RoomDto Room { get; set; } = null!;

        public ICollection<ReservationDayGuestDto> ReservationDayGuests { get; set; }
    }
}
