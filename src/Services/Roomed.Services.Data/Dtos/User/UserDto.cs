namespace Roomed.Services.Data.Dtos.User
{
    using System.ComponentModel.DataAnnotations;

    using static Roomed.Common.DataConstants.ApplicationUser;

    public class UserDto
    {
        public Guid? Id { get; set; }

        [EmailAddress]
        [Required(AllowEmptyStrings = false)]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public string UserName { get; set; }

        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string? Password { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
