namespace Roomed.Services.Data.Dtos.ReservationNote
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Services.Data.Dtos.Reservation;
    using Roomed.Services.Mapping;

    using static Roomed.Common.DataConstants.ReservationNote;

    public class ReservationNoteDto : IMapFrom<Roomed.Data.Models.ReservationNote>
    {
        public Guid Id { get; set; }

        public Guid ReservationId { get; set; }

        [StringLength(BodyMaxLength, MinimumLength = BodyMinLength)]
        public string Body { get; set; } = null!;

        public virtual ReservationDto Reservation { get; set; } = null!;
    }
}
