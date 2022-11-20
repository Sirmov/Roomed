namespace Roomed.Services.Data.Dtos.Profile
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Data.Models.Enums;
    using Roomed.Services.Mapping;

    using static Roomed.Common.DataConstants.Profile;

    public class DetailedProfileDto : IMapFrom<Roomed.Data.Models.Profile>, IMapTo<Roomed.Data.Models.Profile>
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; } = null!;

        [StringLength(MiddleNameMaxLenght, MinimumLength = MiddleNameMinLenght)]
        public string? MiddleName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateOnly Birthdate { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Required]
        [StringLength(NationalityMaxLength, MinimumLength = NationalityMinLength)]
        public string Nationality { get; set; }

        [Required]
        [StringLength(NationalityCodeMaxLength, MinimumLength = NationalityCodeMinLength)]
        public string NationalityCode { get; set; }

        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string? Address { get; set; }
    }
}
