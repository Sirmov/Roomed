namespace Roomed.Web.Areas.Administration.Controllers
{
    using AutoMapper;
    using Ganss.Xss;
    using Microsoft.AspNetCore.Mvc;

    using Roomed.Data.Models;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.User;
    using Roomed.Web.ViewModels.IdentityDocument;
    using Roomed.Web.ViewModels.User;

    using static Roomed.Common.AreasControllersActionsConstants;

    public class UsersController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IUsersService<ApplicationUser, ApplicationRole> usersService;

        public UsersController(
            IHtmlSanitizer sanitizer,
            IUsersService<ApplicationUser, ApplicationRole> usersService,
            IMapper mapper)
            : base(sanitizer)
        {
            this.usersService = usersService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await this.usersService.GetAllUsersAsync();

            var models = users
                .Select(async user =>
                {
                    var model = this.mapper.Map<UserInputModel>(user);
                    model.Roles = await this.usersService.GetUserRolesAsync(user);
                    return model;
                })
                .Select(t => t.Result);

            ViewData["AllRoles"] = await this.usersService.GetAllRolesAsync();

            return View(models);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.SanitizeModel(model);

            var dto = this.mapper.Map<UserDto>(model);

            await this.usersService.EditAsync(dto);

            return RedirectToAction(Actions.Index, Controllers.Users, new { area = Areas.Administration });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new UserInputModel();
            ViewData["AllRoles"] = await this.usersService.GetAllRolesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.SanitizeModel(model);

            if (string.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.AddModelError(nameof(model.Password), "Password is required.");
                return View(model);
            }

            var result = await this.usersService
                .RegisterWithEmailAndUsernameAsync(model.Email, model.UserName, model.Password, model.Roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong.");

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

            return RedirectToAction(Actions.Index, Controllers.Users, new { area = Areas.Administration });
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            ApplicationUser user = null;

            try
            {
                user = await this.usersService.FindUserByIdAsync(id.ToString());
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest();
                throw;
            }

            var model = this.mapper.Map<UserViewModel>(user);
            model.Roles = await this.usersService.GetUserRolesAsync(user);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id, UserViewModel model)
        {
            if (id == model.Id)
            {
                return View(model);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.usersService.DeleteUserWithId(id.ToString());

            return RedirectToAction(Actions.Index, Controllers.Users, new { area = Areas.Administration });
        }
    }
}
