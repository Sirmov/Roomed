﻿namespace Roomed.Web.ViewModels.ReservationDayGuest
{
    using Roomed.Services.Data.Dtos.ReservationDayGuest;
    using Roomed.Services.Mapping;
    using Roomed.Web.ViewModels.Profile;
    using Roomed.Web.ViewModels.ReservationDay;

    public class ReservationDayGuestViewModel : IMapFrom<ReservationDayGuestDto>
    {
        public int Id { get; set; }

        public Guid ReservationDayId { get; set; }

        public Guid GuestId { get; set; }

        public virtual ReservationDayViewModel ReservationDay { get; set; } = null!;

        public virtual ProfileViewModel Guest { get; set; } = null!;
    }
}