namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Data.Common.Models;
    using Roomed.Data.Models.Enums;

    using static Roomed.Common.DataConstants.Profile;

    public class Profile : BaseDeletableModel<Guid>
    {
        public Profile()
        {
            this.Id = Guid.NewGuid();
            this.HolderReservations = new HashSet<Reservation>();
            this.GuestReservationDays = new HashSet<ReservationDayGuest>();
            this.IdentityDocuments = new HashSet<IdentityDocument>();
            this.Notes = new HashSet<ProfileNote>();
        }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ProfileFirstNameMaxLength)]
        public string FirstName { get; set; }

        [MaxLength(ProfileMiddleNameMaxLenght)]
        public string? MiddleName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ProfileLastNameMaxLength)]
        public string LastName { get; set; }

        public DateOnly? Birthdate { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender? Gender { get; set; }

        [MaxLength(ProfileNationalityMaxLength)]
        public string? Nationality { get; set; }

        [StringLength(ProfileNationalityCodeMaxLength, MinimumLength = ProfileNationalityCodeMinLength)]
        public string? NationalityCode { get; set; }

        [StringLength(ProfileAddressMaxLength, MinimumLength = ProfileAddressMinLength)]
        public string? Address { get; set; }

        // Navigational Properties
        public virtual ICollection<Reservation> HolderReservations { get; set; }

        public virtual ICollection<ReservationDayGuest> GuestReservationDays { get; set; }

        public virtual ICollection<IdentityDocument> IdentityDocuments { get; set; }

        public virtual ICollection<ProfileNote> Notes { get; set; }
    }
}
