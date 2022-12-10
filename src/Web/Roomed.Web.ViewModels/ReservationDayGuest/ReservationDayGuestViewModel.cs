// <copyright file="ReservationDayGuestViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
        public int Id { get; set; }

        public Guid ReservationDayId { get; set; }

        public Guid GuestId { get; set; }

        public virtual ReservationDayViewModel ReservationDay { get; set; } = null!;

        public virtual DetailedProfileViewModel Guest { get; set; } = null!;
    }
}
