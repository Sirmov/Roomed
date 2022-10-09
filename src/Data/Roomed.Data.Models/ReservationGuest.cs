namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class ReservationGuest
    {
        public string ReservationId { get; set; }

        public string GuestId { get; set; }

        // Navigational Properties
        [ForeignKey(nameof(ReservationId))]
        public virtual Reservation Reservation { get; set; }

        [ForeignKey(nameof(GuestId))]
        public virtual Profile Guest { get; set; }
    }
}
