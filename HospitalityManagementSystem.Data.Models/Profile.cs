namespace HospitalityManagementSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using HospitalityManagementSystem.Common;
    using HospitalityManagementSystem.Data.Models.Enums;
    using HospitalityManagmentSystem.Data.Common.Models;

    public class Profile : BaseDeletableModel<string>
    {
        public Profile()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Reservations = new HashSet<Reservation>();
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

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
