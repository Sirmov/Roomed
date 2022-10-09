namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Data.Common.Models;

    public class ReservationDay : BaseDeletableModel<Guid>
    {
        public ReservationDay()
        {
            this.Id = Guid.NewGuid();
            this.Guests = new HashSet<ReservationDayGuest>();
        }

        public Guid ReservationId { get; set; }

        public Room Room { get; set; }

        // Navigational Properties
        [ForeignKey(nameof(ReservationId))]
        public virtual Reservation Reservation { get; set; }

        public virtual ICollection<ReservationDayGuest> Guests { get; set; }
    }
}
