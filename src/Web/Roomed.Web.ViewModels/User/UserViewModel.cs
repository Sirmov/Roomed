// |-----------------------------------------------------------------------------------------------------|
// <copyright file="UserViewModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.ViewModels.User
{
    using Microsoft.AspNetCore.Identity;

    using Roomed.Data.Models;
    using Roomed.Services.Data.Dtos.User;
    using Roomed.Services.Mapping;

    /// <summary>
    /// This is a <see cref="ApplicationUser"/> view model.
    /// </summary>
    public class UserViewModel : IMapFrom<ApplicationUser>, IMapTo<UserDto>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public Guid Id { get; set; }

        /// <inheritdoc cref="IdentityUser{TKey}.Email"/>
        public string Email { get; set; } = null!;

        /// <inheritdoc cref="IdentityUser{TKey}.UserName"/>
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Gets or sets a collection of the roles that the user is in.
        /// </summary>
        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
