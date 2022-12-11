// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationDayGuestViewModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.ViewModels.ReservationDayGuest
{
    using Roomed.Services.Data.Dtos.ReservationDayGuest;
    using Roomed.Services.Mapping;
    using Roomed.Web.ViewModels.Profile;
    using Roomed.Web.ViewModels.ReservationDay;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.ReservationDayGuest"/> view model.
    /// </summary>
    public class ReservationDayGuestViewModel : IMapFrom<ReservationDayGuestDto>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public int Id { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.ReservationDayGuest.ReservationDayId"/>
        public Guid ReservationDayId { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.ReservationDayGuest.GuestId"/>
        public Guid GuestId { get; set; }

        /// <summary>
        /// Gets or sets reservation day view model.
        /// </summary>
        public ReservationDayViewModel ReservationDay { get; set; } = null!;

        /// <summary>
        /// Gets or sets guest profile view model.
        /// </summary>
        public DetailedProfileViewModel Guest { get; set; } = null!;
    }
}
