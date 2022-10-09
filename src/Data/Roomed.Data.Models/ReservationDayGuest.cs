namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Data.Common.Models;

    public class ReservationDayGuest : BaseDeletableModel<int>
    {
        public Guid ReservationDayId { get; set; }

        public Guid GuestId { get; set; }

        // Navigational Properties
        [ForeignKey(nameof(ReservationDayId))]
        public virtual ReservationDay ReservationDay { get; set; }

        [ForeignKey(nameof(GuestId))]
        public virtual Profile Guest { get; set; }
    }
}
