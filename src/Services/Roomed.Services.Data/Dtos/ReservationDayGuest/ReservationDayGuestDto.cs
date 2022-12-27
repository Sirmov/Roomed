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

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.ReservationDayGuest"/> data transfer object.
    /// </summary>
    public class ReservationDayGuestDto : IMapFrom<Roomed.Data.Models.ReservationDayGuest>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public int Id { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.ReservationDayGuest.ReservationDayId"/>
        public Guid ReservationDayId { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.ReservationDayGuest.GuestId"/>
        public Guid GuestId { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.ReservationDayGuest.ReservationDay"/>
        public ReservationDayDto ReservationDay { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.ReservationDayGuest.Guest"/>
        public DetailedProfileDto Guest { get; set; } = null!;
    }
}
