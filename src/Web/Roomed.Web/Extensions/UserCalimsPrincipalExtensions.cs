namespace Roomed.Web.Extensions
{
    using System.Security.Claims;

    /// <summary>
    /// This class holds all extension methods for the user claims principal.
    /// </summary>
    public static class UserCalimsPrincipalExtensions
    {
        /// <summary>
        /// This extension method returns the user's id.
        /// </summary>
        /// <param name="user">The current user.</param>
        /// <returns>Returns a <see cref="Guid"/> representing the user id.</returns>
        public static Guid Id(this ClaimsPrincipal user)
        {
            return Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        /// <summary>
        /// This extension method returns the user's username.
        /// </summary>
        /// <param name="user">The current user.</param>
        /// <returns>Returns a <see cref="string"/>. The user's username.</returns>
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.Identity?.Name ?? string.Empty;
        }

        /// <summary>
        /// This extension method determines whether the user is an administrator.
        /// </summary>
        /// <param name="user">The current user.</param>
        /// <returns>Returns a <see cref="bool"/> indicating whether the user is an admin.</returns>
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole("Administrator");
        }
    }
}
