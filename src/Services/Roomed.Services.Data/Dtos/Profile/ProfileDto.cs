namespace Roomed.Services.Data.Dtos.Profile
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Services.Mapping;

    using static Roomed.Common.DataConstants.Profile;

    public class ProfileDto : IMapFrom<Roomed.Data.Models.Profile>
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; } = null!;
    }
}
