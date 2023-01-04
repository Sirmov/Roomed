// |-----------------------------------------------------------------------------------------------------|
// <copyright file="LoginViewModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    using static Roomed.Common.Constants.DataConstants.ApplicationUser;

    /// <summary>
    /// This is a login view model. It is used for displaying the login form and validation errors.
    /// It is also used as an input model.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the login username.
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the login password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string Password { get; set; } = null!;

        /// <summary>
        /// Gets or sets a value indicating whether the session should be kept longer.
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
