namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Data.Common.Models;

    using static Roomed.Data.Common.DataConstants.Reservation;

    public class Reservation : BaseDeletableModel<Guid>
    {
        public Reservation()
        {
            this.Id = Guid.NewGuid();
            this.ReservationDays = new HashSet<ReservationDay>();
            this.Notes = new HashSet<ReservationNote>();
        }

        [Required(AllowEmptyStrings = false)]
        public Guid ReservationHolderId { get; set; }

        [Required]
        public DateOnly ArrivalDate { get; set; }

        [Required]
        public DateOnly DepartureDate { get; set; }

        [Required]
        public RoomType RoomType { get; set; }

        [Required]
        [Range(0, ReservationAdultsMaxCount)]
        public int Adults { get; set; }

        [Required]
        [Range(0, ReservationTeenagersMaxCount)]
        public int Teenagers { get; set; }

        [Required]
        [Range(0, ReservationChildrenMaxCount)]
        public int Children { get; set; }

        // Navigational Properties
        [ForeignKey(nameof(ReservationHolderId))]
        public virtual Profile ReservationHolder { get; set; }

        public virtual ICollection<ReservationDay> ReservationDays { get; set; }

        public virtual ICollection<ReservationNote> Notes { get; set; }
    }
}
