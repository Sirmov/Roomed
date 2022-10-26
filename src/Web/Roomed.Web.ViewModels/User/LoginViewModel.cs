namespace Roomed.Web.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    using static Roomed.Common.DataConstants.ApplicationUser;

    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength (PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
