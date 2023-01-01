// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationViewModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.ViewModels.Reservation
{
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Dtos.Reservation;
    using Roomed.Services.Mapping;
    using Roomed.Web.ViewModels.Profile;
    using Roomed.Web.ViewModels.ReservationDay;
    using Roomed.Web.ViewModels.RoomType;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.Reservation"/> view model.
    /// </summary>
    public class ReservationViewModel : IMapFrom<ReservationDto>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public Guid Id { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.ReservationHolderId"/>
        public Guid ReservationHolderId { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.ArrivalDate"/>
        public DateOnly ArrivalDate { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.DepartureDate"/>
        public DateOnly DepartureDate { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.Status"/>
        public ReservationStatus Status { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.RoomTypeId"/>
        public int RoomTypeId { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.Adults"/>
        public int Adults { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.Teenagers"/>
        public int Teenagers { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Reservation.Children"/>
        public int Children { get; set; }

        /// <summary>
        /// Gets or sets reservation holder view model.
        /// </summary>
        public DetailedProfileViewModel ReservationHolder { get; set; } = null!;

        /// <summary>
        /// Gets or sets room type view model.
        /// </summary>
        public RoomTypeViewModel RoomType { get; set; } = null!;

        /// <summary>
        /// Gets or sets a collection of reservation day view models.
        /// </summary>
        public ICollection<ReservationDayViewModel> ReservationDays { get; set; } = new List<ReservationDayViewModel>();
    }
}
