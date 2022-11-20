namespace Roomed.Web.Controllers
{
    using Ganss.Xss;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Roomed.Data.Models;
    using Roomed.Services.Data.Contracts;
    using Roomed.Web.ViewModels.User;

    using static Roomed.Common.ControllersActionsConstants;

    /// <summary>
    /// A MVC controller inheriting <see cref="BaseController"/>.
    /// The user controller is responsible for all operations regarding the user identity.
    /// </summary>
    public class UserController : BaseController
    {
        private readonly IUsersService<ApplicationUser> usersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// Uses constructor injection to resolve dependencies.
        /// </summary>
        /// <param name="usersService">The implementation of <see cref="IUsersService{TUser}"/>.</param>
        public UserController(IUsersService<ApplicationUser> usersService, IHtmlSanitizer sanitizer)
            : base(sanitizer)
        {
            this.usersService = usersService;
        }

        /// <summary>
        /// This method returns the login page.
        /// </summary>
        /// <returns>Return <see cref="IActionResult"/>.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var viewModel = new LoginViewModel();

            return View(viewModel);
        }

        /// <summary>
        /// This method handles the login request.
        /// Returns the same view if the login is not successful otherwise redirects to <see cref="HomeController.Index"/> action.
        /// </summary>
        /// <param name="inputModel">The login input model.</param>
        /// <returns>Returns a task of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid login!");
                return View(inputModel);
            }

            try
            {
                var result = await this.usersService.LoginWithUsernameAsync(inputModel.UserName, inputModel.Password, inputModel.RememberMe, true);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login!");

                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "Too many attempts! Try again later!");
                    }

                    return View(inputModel);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Invalid login!");
                return View(inputModel);
            }

            return RedirectToAction(Actions.Index, Controllers.Home);
        }

        /// <summary>
        /// This method signs out the current user.
        /// Redirects to <see cref="UserController.Login"/> action.
        /// </summary>
        /// <returns>Returns a task of <see cref="IActionResult"/>.</returns>
        public async Task<IActionResult> Logout()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                await usersService.LogoutAsync();
            }

            return RedirectToAction(Actions.Login);
        }
    }
}
