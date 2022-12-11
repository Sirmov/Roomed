// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationDayViewModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.ViewModels.ReservationDay
{
    using Roomed.Services.Data.Dtos.ReservationDay;
    using Roomed.Services.Mapping;
    using Roomed.Web.ViewModels.Reservation;
    using Roomed.Web.ViewModels.ReservationDayGuest;
    using Roomed.Web.ViewModels.Room;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.ReservationDay"/> view model.
    /// </summary>
    public class ReservationDayViewModel : IMapFrom<ReservationDayDto>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public Guid Id { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.ReservationDay.ReservationId"/>
        public Guid ReservationId { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.ReservationDay.RoomId"/>
        public int RoomId { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.ReservationDay.Date"/>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Gets or sets reservation view model.
        /// </summary>
        public ReservationViewModel Reservation { get; set; } = null!;

        /// <summary>
        /// Gets or sets room view model.
        /// </summary>
        public RoomViewModel Room { get; set; } = null!;

        /// <summary>
        /// Gets or sets a collection of reservation day guest view models.
        /// </summary>
        public ICollection<ReservationDayGuestViewModel> ReservationDayGuests { get; set; } = new List<ReservationDayGuestViewModel>();
    }
}
