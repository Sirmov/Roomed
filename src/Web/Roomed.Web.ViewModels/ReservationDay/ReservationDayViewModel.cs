// <copyright file="ReservationDayViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
        public Guid Id { get; set; }

        public Guid ReservationId { get; set; }

        public int RoomId { get; set; }

        public DateOnly Date { get; set; }

        public ReservationViewModel Reservation { get; set; } = null!;

        public RoomViewModel Room { get; set; } = null!;

        public ICollection<ReservationDayGuestViewModel> ReservationDayGuests { get; set; }
    }
}
