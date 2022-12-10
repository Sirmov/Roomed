// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationDayGuestDto.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Dtos.ReservationDayGuest
{
    using Roomed.Services.Data.Dtos.Profile;
    using Roomed.Services.Data.Dtos.ReservationDay;
    using Roomed.Services.Mapping;

    public class ReservationDayGuestDto : IMapFrom<Roomed.Data.Models.ReservationDayGuest>
    {
        public int Id { get; set; }

        public Guid ReservationDayId { get; set; }

        public Guid GuestId { get; set; }

        public virtual ReservationDayDto ReservationDay { get; set; } = null!;

        public virtual DetailedProfileDto Guest { get; set; } = null!;
    }
}
