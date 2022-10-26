namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Data.Common.Models;

    using static Roomed.Common.DataConstants.ReservationNote;

    public class ReservationNote : BaseDeletableModel<Guid>
    {
        public ReservationNote()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid ReservationId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ReservationNoteBodyMaxLength)]
        public string Body { get; set; }

        // Navigational Properties
        [ForeignKey(nameof(ReservationId))]
        public virtual Reservation Reservation { get; set; }
    }
}
