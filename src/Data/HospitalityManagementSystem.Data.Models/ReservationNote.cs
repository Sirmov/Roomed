namespace HospitalityManagementSystem.Data.Models
{
    using HospitalityManagementSystem.Common;
    using HospitalityManagmentSystem.Data.Common.Models;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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
