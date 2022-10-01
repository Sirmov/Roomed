namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Common;
    using Roomed.Data.Common.Models;

    public class Reservation : BaseDeletableModel<string>
    {
        public Reservation()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Guests = new HashSet<ReservationGuest>();
            this.Notes = new HashSet<ReservationNote>();
        }

        [Required(AllowEmptyStrings = false)]
        public string ReservationHolderId { get; set; }

        [Required]
        public DateOnly ArrivalDate { get; set; }

        [Required]
        public DateOnly DepartureDate { get; set; }

        [Required]
        [Range(0, GlobalConstants.ReservationAdultsMaxCount)]
        public int Adults { get; set; }

        [Required]
        [Range(0, GlobalConstants.ReservationTeenagersMaxCount)]
        public int Teenagers { get; set; }

        [Required]
        [Range(0, GlobalConstants.ReservationChildrenMaxCount)]
        public int Children { get; set; }

        // Navigational Properties
        [ForeignKey(nameof(ReservationHolderId))]
        public virtual Profile ReservationHolder { get; set; }

        public virtual ICollection<ReservationGuest> Guests { get; set; }

        public virtual ICollection<ReservationNote> Notes { get; set; }
    }
}
