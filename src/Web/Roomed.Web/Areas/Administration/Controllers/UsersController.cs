namespace Roomed.Web.Areas.Administration.Controllers
{
    using Ganss.Xss;
    using Microsoft.AspNetCore.Mvc;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Contracts;

    public class UsersController : BaseController
    {
        private readonly IUsersService<ApplicationUser> usersService;

        public UsersController(
            IHtmlSanitizer sanitizer,
            IUsersService<ApplicationUser> usersService)
            : base(sanitizer)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await this.usersService.GetAllUsersAsync();

            return View(users);
        }
    }
}
