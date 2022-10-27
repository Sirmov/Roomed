namespace Roomed.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Roomed.Data.Models;
    using Roomed.Services.Data.Contracts;
    using Roomed.Web.ViewModels.User;

    using static Roomed.Common.ControllersActionsConstants;

    public class UserController : Controller
    {
        private readonly IUsersService<ApplicationUser> usersService;

        public UserController(IUsersService<ApplicationUser> usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var viewModel = new LoginViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid login!");
                return View(inputModel);
            }

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

            return RedirectToAction(Actions.Index, Controllers.Home);
        }

        [Authorize]
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
