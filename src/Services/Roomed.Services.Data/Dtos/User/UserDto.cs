// |-----------------------------------------------------------------------------------------------------|
// <copyright file="UserDto.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Dtos.User
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    using static Roomed.Common.Constants.DataConstants.ApplicationUser;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.ApplicationUser"/> data transfer object.
    /// </summary>
    public class UserDto
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public Guid? Id { get; set; }

        /// <inheritdoc cref="IdentityUser{TKey}.Email"/>
        [EmailAddress]
        [Required(AllowEmptyStrings = false)]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
        public string Email { get; set; } = null!;

        /// <inheritdoc cref="IdentityUser{TKey}.UserName"/>
        [Required(AllowEmptyStrings = false)]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the password of the user. Used when creating a new user.
        /// </summary>
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets the collection of the roles of the user.
        /// </summary>
        public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
    }
}
