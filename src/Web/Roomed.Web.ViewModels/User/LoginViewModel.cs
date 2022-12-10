// <copyright file="LoginViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Roomed.Web.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    using static Roomed.Common.DataConstants.ApplicationUser;

    /// <summary>
    /// This is a login view model. It is used for displaying the login form and validation errors.
    /// It is also used as an input model.
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
