// <copyright file="ReservationViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
        public Guid Id { get; set; }

        public Guid ReservationHolderId { get; set; }

        public DateOnly ArrivalDate { get; set; }

        public DateOnly DepartureDate { get; set; }

        public ReservationStatus Status { get; set; }

        public int RoomTypeId { get; set; }

        public DetailedProfileViewModel ReservationHolder { get; set; } = null!;

        public RoomTypeViewModel RoomType { get; set; } = null!;

        public ICollection<ReservationDayViewModel> ReservationDays { get; set; }
    }
}
