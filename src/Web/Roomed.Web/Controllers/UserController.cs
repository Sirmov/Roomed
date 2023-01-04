// |-----------------------------------------------------------------------------------------------------|
// <copyright file="UserController.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.Controllers
{
    using Ganss.Xss;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Roomed.Data.Models;
    using Roomed.Services.Data.Contracts;
    using Roomed.Web.ViewModels.User;

    using static Roomed.Common.Constants.AreasControllersActionsConstants;

    /// <summary>
    /// A MVC controller inheriting <see cref="BaseController"/>.
    /// The user controller is responsible for all operations regarding the user identity.
    /// </summary>
    public class UserController : BaseController
    {
        private readonly IUsersService<ApplicationUser, ApplicationRole> usersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// Uses constructor injection to resolve dependencies.
        /// </summary>
        /// <param name="usersService">The implementation of <see cref="IUsersService{TUser, TRole}"/>.</param>
        /// <param name="sanitizer">The implementation of <see cref="IHtmlSanitizer"/>.</param>
        public UserController(
            IUsersService<ApplicationUser, ApplicationRole> usersService,
            IHtmlSanitizer sanitizer)
            : base(sanitizer)
        {
            this.usersService = usersService;
        }

        /// <summary>
        /// This action returns the login page.
        /// </summary>
        /// <returns>Return the login view.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var viewModel = new LoginViewModel();

            return View(viewModel);
        }

        /// <summary>
        /// This action handles the login request.
        /// Returns the same view if the login is not successful otherwise redirects to <see cref="HomeController.Index"/> action.
        /// </summary>
        /// <param name="inputModel">The login input model.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="IActionResult"/>.</returns>
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
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Invalid login!");
                return View(inputModel);
            }

            return RedirectToAction(Actions.Index, Controllers.Home);
        }

        /// <summary>
        /// This action signs out the current user.
        /// Redirects to <see cref="UserController.Login()"/> action.
        /// </summary>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="IActionResult"/>.</returns>
        public async Task<IActionResult> Logout()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                await usersService.LogoutAsync();
            }

            return RedirectToAction(Actions.Login);
        }

        /// <summary>
        /// This action returns the access denied page.
        /// </summary>
        /// <returns>Return the access denied view.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
