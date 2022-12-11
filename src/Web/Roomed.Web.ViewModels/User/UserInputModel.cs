// |-----------------------------------------------------------------------------------------------------|
// <copyright file="UserInputModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    using Roomed.Common.Attribues;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Dtos.User;
    using Roomed.Services.Mapping;

    using static Roomed.Common.DataConstants.ApplicationUser;

    /// <summary>
    /// This is a <see cref="ApplicationUser"/> input model.
    /// </summary>
    public class UserInputModel : IMapFrom<ApplicationUser>, IMapTo<UserDto>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public Guid? Id { get; set; }

        /// <inheritdoc cref="IdentityUser{TKey}.Email"/>
        [Required(AllowEmptyStrings = false)]
        [Sanitize]
        [EmailAddress]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        /// <inheritdoc cref="IdentityUser{TKey}.UserName"/>
        [Required(AllowEmptyStrings = false)]
        [Sanitize]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        [Display(Name = "Username")]
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        [Sanitize]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets a collection of the roles that the user is in.
        /// </summary>
        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
