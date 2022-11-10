namespace Roomed.Web.ViewModels.ReservationDay
{
    using Roomed.Web.ViewModels.Reservation;
    using Roomed.Web.ViewModels.Room;

    public class ReservationDayViewModel
    {
        public Guid ReservationId { get; set; }

        public int RoomId { get; set; }

        public DateOnly Date { get; set; }

        public ReservationViewModel Reservation { get; set; } = null!;

        public RoomViewModel Room { get; set; } = null!;

        public ICollection<ReservationDayGuest> ReservationDayGuests { get; set; }
    }
}
