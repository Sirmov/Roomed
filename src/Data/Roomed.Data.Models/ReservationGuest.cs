namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Data.Common.Models;

    public class ReservationGuest : BaseDeletableModel<int>
    {
        public Guid ReservationId { get; set; }

        public Guid GuestId { get; set; }

        // Navigational Properties
        [ForeignKey(nameof(ReservationId))]
        public virtual Reservation Reservation { get; set; }

        [ForeignKey(nameof(GuestId))]
        public virtual Profile Guest { get; set; }
    }
}
