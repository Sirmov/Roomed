namespace Roomed.Web.ViewModels.Reservation
{
    using Roomed.Web.ViewModels.Enums;
    using Roomed.Web.ViewModels.Profile;
    using Roomed.Web.ViewModels.RoomType;

    public class ReservationViewModel
    {
        public Guid ReservationHolderId { get; set; }

        public DateOnly ArrivalDate { get; set; }

        public DateOnly DepartureDate { get; set; }

        public ReservationStatus Status { get; set; }

        public int RoomTypeId { get; set; }

        public ProfileViewModel ReservationHolder { get; set; } = null!;

        public RoomTypeViewModel RoomType { get; set; } = null!;

        public ICollection<ReservationViewModel> ReservationDays { get; set; }
    }
}
