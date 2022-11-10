namespace Roomed.Services.Data.Dtos.Reservation
{
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Dtos.Profile;
    using Roomed.Services.Data.Dtos.ReservationDay;
    using Roomed.Services.Data.Dtos.RoomType;
    using Roomed.Services.Mapping;

    public class ReservationDto : IMapFrom<Roomed.Data.Models.Reservation>
    {
        public Guid Id { get; set; }

        public Guid ReservationHolderId { get; set; }

        public DateOnly ArrivalDate { get; set; }

        public DateOnly DepartureDate { get; set; }

        public ReservationStatus Status { get; set; }

        public int RoomTypeId { get; set; }

        public ProfileDto ReservationHolder { get; set; } = null!;

        public RoomTypeDto RoomType { get; set; } = null!;

        public ICollection<ReservationDayDto> ReservationDays { get; set; }
    }
}
