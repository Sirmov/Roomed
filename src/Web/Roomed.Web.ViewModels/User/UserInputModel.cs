namespace Roomed.Web.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Common.Attribues;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Dtos.User;
    using Roomed.Services.Mapping;

    using static Roomed.Common.DataConstants.ApplicationUser;

    public class UserInputModel : IMapFrom<ApplicationUser>, IMapTo<UserDto>
    {
        public Guid? Id { get; set; }

        [Sanitize]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false)]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
        public string Email { get; set; }

        [Sanitize]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Username")]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public string UserName { get; set; }

        [Sanitize]
        [DataType(DataType.Password)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string? Password { get; set; }

        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
