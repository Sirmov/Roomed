namespace Roomed.Web.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
