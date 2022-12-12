// |-----------------------------------------------------------------------------------------------------|
// <copyright file="UsersService.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using Roomed.Data.Models;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.User;

    /// <summary>
    /// Implementation of the IUsersService.
    /// Abstraction on top of the user manager and sign in manager from Microsoft Identity.
    /// </summary>
    /// <typeparam name="TUser">Class inheritor of <see cref="ApplicationUser"/> with parameterless constructor.</typeparam>
    /// <typeparam name="TRole">Class inheritor of <see cref="ApplicationRole"/> with parameterless constructor.</typeparam>
    public class UsersService<TUser, TRole> : IUsersService<TUser, TRole>
        where TUser : ApplicationUser, new()
        where TRole : ApplicationRole, new()
    {
        private readonly UserManager<TUser> userManager;
        private readonly SignInManager<TUser> signInManager;
        private readonly RoleManager<TRole> roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersService{TUser, TRole}"/> class.
        /// Injects user, role and sign in manager from IoC.
        /// </summary>
        /// <param name="userManager">Microsoft Identity user manager.</param>
        /// <param name="signInManager">Microsoft Identity sign in manager.</param>
        /// <param name="roleManager">Microsoft Identity role manager.</param>
        public UsersService(
            UserManager<TUser> userManager,
            SignInManager<TUser> signInManager,
            RoleManager<TRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Throws when any of the arguments is null or empty.</exception>
        public async Task<IdentityResult> RegisterWithEmailAndUsernameAsync(string email, string username, string password, IEnumerable<string>? roles = null)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email), "Email cannot be null or white space.");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException(nameof(username), "Username cannot be null or white space.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password), "Password cannot be null or white space.");
            }

            TUser user = this.CreateUserWithEmailAndUsername(email, username);

            var result = await this.userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return result;
            }

            if (roles != null)
            {
                user = await this.FindUserByEmailAsync(email);

                foreach (var role in roles)
                {
                    await this.AddToRoleAsync(user, role);
                }
            }

            return result;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Throws when the email or the password is null or empty.</exception>
        public async Task<SignInResult> LoginWithEmailAsync(string email, string password, bool isPersistant = false, bool isLockout = true)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email), "Email cannot be null or white space.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password), "Password cannot be null or white space.");
            }

            var user = await this.FindUserByEmailAsync(email);
            var result = await this.signInManager.PasswordSignInAsync(user, password, isPersistant, isLockout);

            return result;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Throws when the username or the password is null or empty.</exception>
        public async Task<SignInResult> LoginWithUsernameAsync(string username, string password, bool isPersistant = false, bool isLockout = true)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException(nameof(username), "Username cannot be null or white space.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password), "Password cannot be null or white space.");
            }

            var user = await this.FindUserByUsernameAsync(username);
            var result = await this.signInManager.PasswordSignInAsync(user, password, isPersistant, isLockout);

            return result;
        }

        /// <inheritdoc/>
        public async Task LogoutAsync()
        {
            await this.signInManager.SignOutAsync();
        }

        /// <inheritdoc/>
        public bool IsSignedIn(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            var principal = user as ClaimsPrincipal;

            return this.signInManager.IsSignedIn(principal);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Throws when the email is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Throws when no user with this email can be found.</exception>
        public async Task<TUser> FindUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email), "Email cannot be null or white space.");
            }

            var user = await this.userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new InvalidOperationException("No user with this email was found.");
            }

            return user;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Throws when the id is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Throws when no user with this id can be found.</exception>
        public async Task<TUser> FindUserByIdAsync(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Id cannot be null.");
            }

            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new InvalidOperationException("No user with this id was found.");
            }

            return user;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Throws when the username is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Throws when no user with this username can be found.</exception>
        public async Task<TUser> FindUserByUsernameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException(nameof(username), "Username cannot be null or white space.");
            }

            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                throw new InvalidOperationException("No user with this username was found.");
            }

            return user;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Throws when any of the arguments is null or empty.</exception>
        public TUser CreateUserWithEmailAndUsername(string email, string username)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email), "Email cannot be null or white space.");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException(nameof(username), "Username cannot be null or white space.");
            }

            var user = Activator.CreateInstance<TUser>();
            user.Email = email;
            user.UserName = username;
            return user;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TUser>> GetAllUsersAsync()
        {
            return await this.userManager.Users.ToListAsync();
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Throws when the user is null.</exception>
        public async Task<IEnumerable<string>> GetUserRolesAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var roles = await userManager.GetRolesAsync(user);

            return roles;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string>> GetAllRolesAsync()
        {
            var roles = await this.roleManager.Roles.ToListAsync();

            return roles.Select(r => r.Name);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Throws when the user dto or user id is null.</exception>
        /// <exception cref="ArgumentException">Throws when the user model state is invalid.</exception>
        public async Task EditAsync(UserDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }

            bool isValid = this.ValidateDto(userDto);

            if (!isValid)
            {
                throw new ArgumentException("User model state is not valid.", nameof(userDto));
            }

            string id = userDto?.Id?.ToString() ?? throw new ArgumentNullException(nameof(userDto.Id));

            var user = await this.FindUserByIdAsync(id);

            user.Email = userDto.Email;
            user.UserName = userDto.UserName;

            foreach (var role in userDto.Roles)
            {
                await this.AddToRoleAsync(user, role);
            }

            foreach (var userRole in await this.GetUserRolesAsync(user))
            {
                if (!userDto.Roles.Contains(userRole))
                {
                    await this.RemoveFromRoleAsync(user, userRole);
                }
            }

            await this.userManager.UpdateAsync(user);
        }

        /// <inheritdoc/>
        public async Task<bool> DoesRoleExistAsync(string role)
        {
            return await this.roleManager.RoleExistsAsync(role);
        }

        /// <inheritdoc/>
        public async Task AddToRoleAsync(TUser user, string role)
        {
            if (await this.DoesRoleExistAsync(role) && !await this.userManager.IsInRoleAsync(user, role))
            {
                await this.userManager.AddToRoleAsync(user, role);
            }
        }

        /// <inheritdoc/>
        public async Task RemoveFromRoleAsync(TUser user, string role)
        {
            if (await this.DoesRoleExistAsync(role) && await this.userManager.IsInRoleAsync(user, role))
            {
                await this.userManager.RemoveFromRoleAsync(user, role);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Throws when the id is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Throws when no user with this id can be found.</exception>
        public async Task<IdentityResult> DeleteUserWithId(string id)
        {
            var user = await this.FindUserByIdAsync(id);

            var result = await this.userManager.DeleteAsync(user);

            return result;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Throws when the id is null.</exception>
        public async Task<bool> ExistsAsync(string id)
        {
            var result = true;

            try
            {
                await this.FindUserByIdAsync(id);
            }
            catch (InvalidOperationException)
            {
                result = false;
            }

            return result;
        }

        private bool ValidateDto<TDto>(TDto dto)
        {
            var context = new ValidationContext(dto, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, context, validationResults, true);

            return isValid;
        }
    }
}
