namespace Roomed.Services.Data.Contracts
{
    using Microsoft.AspNetCore.Identity;
    using Roomed.Data.Models;

    /// <summary>
    /// This interface is used to state and document the users data service functionality.
    /// </summary>
    /// <typeparam name="TUser">Class inheritor of <see cref="ApplicationUser"/> with parameterless constructor.</typeparam>
    public interface IUsersService<TUser>
        where TUser : ApplicationUser, new()
    {
        /// <summary>
        /// This method returns all registered users asynchronously.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> with a collection of all <typeparamref name="TUser"/> users.</returns>
        public Task<IEnumerable<TUser>> GetAllUsersAsync();

        /// <summary>
        /// This method creates a user with the specified email, username and password, adds it to the database and returns the result.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>Returns a task of <see cref="IdentityResult"/> for the operation.</returns>
        public Task<IdentityResult> RegisterWithEmailAndUsernameAsync(string email, string username, string password);

        /// <summary>
        /// This method tries to log in the user with the specified email and password if the user exists.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The login attempt password.</param>
        /// <param name="isPersistant">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <param name="isLockout">Flag indicating if the user account should be locked if the sign in fails.</param>
        /// <returns>Returns a task of <see cref="SignInResult"/> for the operation.</returns>
        public Task<SignInResult> LoginWithEmailAsync(string email, string password, bool isPersistant = false, bool isLockout = true);

        /// <summary>
        /// This method tries to log in the user with the specified username and password if the user exists.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The login attempt password.</param>
        /// <param name="isPersistant">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <param name="isLockout">Flag indicating if the user account should be locked if the sign in fails.</param>
        /// <returns>Returns a task of <see cref="SignInResult"/> for the operation.</returns>
        public Task<SignInResult> LoginWithUsernameAsync(string username, string password, bool isPersistant = false, bool isLockout = true);

        /// <summary>
        /// This method signs the current user out of the application.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        public Task LogoutAsync();

        /// <summary>
        /// This methods check if the specified user is logged in.
        /// </summary>
        /// <param name="user">The user to check.</param>
        /// <returns>Returns whether the specified user is logged in.</returns>
        public bool IsSignedIn(TUser user);

        /// <summary>
        /// This method tries to find the user with the specified email.
        /// </summary>
        /// <param name="email">The email of the searched user.</param>
        /// <returns>Returns a task of <typeparamref name="TUser"/>.</returns>
        public Task<TUser> FindUserByEmailAsync(string email);

        /// <summary>
        /// This method tries to find the user with the specified username.
        /// </summary>
        /// <param name="username">The username of the searched user.</param>
        /// <returns>Returns a task of <typeparamref name="TUser"/>.</returns>
        public Task<TUser> FindUserByUsernameAsync(string username);

        /// <summary>
        /// This method tries to find the user with the specified id.
        /// </summary>
        /// <param name="id">The id of the searched user.</param>
        /// <returns>Returns a task of <typeparamref name="TUser"/>.</returns>
        public Task<TUser> FindUserByIdAsync(string id);

        /// <summary>
        /// This method creates an instance of the <typeparamref name="TUser"/> class with the specified email and username.
        /// </summary>
        /// <param name="email">The email of the new user.</param>
        /// <param name="username">The username of the new user.</param>
        /// <returns>Returns the newly created user.</returns>
        public TUser CreateUserWithEmailAndUsername(string email, string username);
    }
}
