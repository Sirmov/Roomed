namespace Roomed.Services.Data
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Contracts;

    /// <summary>
    /// Implementation of the IUsersService.
    /// Abstraction on top of the user manager and sign in manager from Microsoft Identity.
    /// </summary>
    /// <typeparam name="TUser">Class inheritor of <see cref="ApplicationUser"/> with parameterless constructor.</typeparam>
    public class UsersService<TUser> : IUsersService<TUser>
        where TUser : ApplicationUser, new()
    {
        private readonly UserManager<TUser> userManager;
        private readonly SignInManager<TUser> signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersService{TUser}"/> class.
        /// Injects user and sign in manager from IoC.
        /// </summary>
        /// <param name="userManager">Microsoft Identity user manager.</param>
        /// <param name="signInManager">Microsoft Identity sign in manager.</param>
        public UsersService(UserManager<TUser> userManager, SignInManager<TUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Throws when any of the arguments is null or empty.</exception>
        public async Task<IdentityResult> RegisterWithEmailAndUsernameAsync(string email, string username, string password)
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
    }
}
