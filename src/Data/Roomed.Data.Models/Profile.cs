namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Common;
    using Roomed.Data.Common.Models;
    using Roomed.Data.Models.Enums;

    public class Profile : BaseDeletableModel<string>
    {
        public Profile()
        {
            this.Id = Guid.NewGuid().ToString();
            this.HolderReservations = new HashSet<Reservation>();
            this.GuestReservations = new HashSet<ReservationGuest>();
            this.IdentityDocuments = new HashSet<IdentityDocument>();
            this.Notes = new HashSet<ProfileNote>();
        }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(GlobalConstants.ProfileFirstNameMaxLength)]
        public string FirstName { get; set; }

        [MaxLength(GlobalConstants.ProfileMiddleNameMaxLenght)]
        public string? MiddleName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(GlobalConstants.ProfileLastNameMaxLength)]
        public string LastName { get; set; }

        public DateOnly? Birthdate { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender? Gender { get; set; }

        [MaxLength(GlobalConstants.ProfileNationalityMaxLength)]
        public string? Nationality { get; set; }

        [StringLength(GlobalConstants.ProfileNationalityCodeMaxLength, MinimumLength = GlobalConstants.ProfileNationalityCodeMinLength)]
        public string? NationalityCode { get; set; }

        [StringLength(GlobalConstants.ProfileAddressMaxLength, MinimumLength = GlobalConstants.ProfileAddressMinLength)]
        public string? Address { get; set; }

        // Navigational Properties

        public virtual ICollection<Reservation> HolderReservations { get; set; }

        public virtual ICollection<ReservationGuest> GuestReservations { get; set; }

        public virtual ICollection<IdentityDocument> IdentityDocuments { get; set; }

        public virtual ICollection<ProfileNote> Notes { get; set; }
    }
}
