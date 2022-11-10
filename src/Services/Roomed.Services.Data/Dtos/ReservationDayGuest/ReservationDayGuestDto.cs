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

        public virtual ProfileDto Guest { get; set; } = null!;
    }
}
