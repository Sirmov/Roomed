namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Common;
    using Roomed.Data.Common.Models;

    public class ReservationNote : BaseDeletableModel<string>
    {
        public ReservationNote()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string ReservationId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(GlobalConstants.ReservationNoteBodyMaxLength)]
        public string Body { get; set; }

        // Navigational Properties

        [ForeignKey(nameof(ReservationId))]
        public virtual Reservation Reservation { get; set; }
    }
}
