namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Data.Common.Models;

    /// <summary>
    /// Reservation day guest mapping entity model. Inherits base deletable model. Has int id.
    /// </summary>
    public class ReservationDayGuest : BaseDeletableModel<int>
    {
        /// <summary>
        /// Gets or sets reservation day id foreign key.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public Guid ReservationDayId { get; set; }

        /// <summary>
        /// Gets or sets guest id foreign key.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public Guid GuestId { get; set; }

        // Navigational Properties

        /// <summary>
        /// Gets or sets reservation day navigational property.
        /// </summary>
        [ForeignKey(nameof(ReservationDayId))]
        public virtual ReservationDay ReservationDay { get; set; } = null!;

        /// <summary>
        /// Gets or sets guest navigational property.
        /// </summary>
        [ForeignKey(nameof(GuestId))]
        public virtual Profile Guest { get; set; } = null!;
    }
}
