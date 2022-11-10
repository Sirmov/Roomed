namespace Roomed.Services.Data.Dtos.ReservationNote
{
    using Roomed.Services.Data.Dtos.Reservation;
    using Roomed.Services.Mapping;

    public class ReservationNoteDto : IMapFrom<Roomed.Data.Models.ReservationNote>
    {
        public Guid Id { get; set; }

        public Guid ReservationId { get; set; }

        public string Body { get; set; } = null!;

        public virtual ReservationDto Reservation { get; set; } = null!;
    }
}
