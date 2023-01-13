// |-----------------------------------------------------------------------------------------------------|
// <copyright file="UsersController.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.Areas.Administration.Controllers
{
    using AutoMapper;
    using Ganss.Xss;
    using Microsoft.AspNetCore.Mvc;

    using Roomed.Common.Constants;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.User;
    using Roomed.Web.ViewModels.User;

    using static Roomed.Common.Constants.AreasControllersActionsConstants;

    /// <summary>
    /// A MVC controller inheriting <see cref="BaseController"/>.
    /// The users controller is responsible for administrating the user profiles of all employees.
    /// </summary>
    public class UsersController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IUsersService<ApplicationUser, ApplicationRole> usersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="sanitizer">The implementation of <see cref="IHtmlSanitizer"/>.</param>
        /// <param name="usersService">The implementation of <see cref="IUsersService{TUser, TRole}"/>.</param>
        /// <param name="mapper">The implementation of <see cref="IMapper"/>.</param>
        public UsersController(
            IHtmlSanitizer sanitizer,
            IUsersService<ApplicationUser, ApplicationRole> usersService,
            IMapper mapper)
            : base(sanitizer)
        {
            this.usersService = usersService;
            this.mapper = mapper;
        }

        /// <summary>
        /// This action returns a page with all user profiles.
        /// </summary>
        /// <returns>Returns a view containing all user profiles.</returns>
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

            ViewData[DataKeyConstants.AllRoles] = await this.usersService.GetAllRolesAsync();

            return View(models);
        }

        /// <summary>
        /// This action handles the edit request.
        /// It validates the model, sanitizes it and modifies the existing user.
        /// Returns the same view if the validation fails otherwise redirects to <see cref="UsersController.Index"/> action.
        /// </summary>
        /// <param name="model">The user input model.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(UserInputModel model)
        {
            if (!await this.usersService.ExistsAsync(model.Id?.ToString() ?? string.Empty))
            {
                return base.ShowError("An error occurred", "The user you are trying to edit cannot be found.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.SanitizeModel(model);

            var dto = this.mapper.Map<UserDto>(model);

            await this.usersService.EditAsync(dto);

            return RedirectToAction(Actions.Index, Controllers.Users, new { area = Areas.Administration });
        }

        /// <summary>
        /// This action returns a page with a form for creating a new user.
        /// </summary>
        /// <returns>Returns the create user view.</returns>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new UserInputModel();
            ViewData[DataKeyConstants.AllRoles] = await this.usersService.GetAllRolesAsync();

            return View(model);
        }

        /// <summary>
        /// This action handles the create request.
        /// It validates the model, sanitizes it and adds it to the database.
        /// Returns the same view if the validation fails otherwise redirects to <see cref="UsersController.Index"/> action.
        /// </summary>
        /// <param name="model">The user input model.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="IActionResult"/>.</returns>
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

        /// <summary>
        /// This action returns a page with details of an existing user.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>Returns the details user view.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            if (!await this.usersService.ExistsAsync(id.ToString()))
            {
                return base.ShowError("An error occurred", "The user you are trying to view cannot be found.");
            }

            ApplicationUser user = await this.usersService.FindUserByIdAsync(id.ToString());

            var model = this.mapper.Map<UserViewModel>(user);
            model.Roles = await this.usersService.GetUserRolesAsync(user);

            return View(model);
        }

        /// <summary>
        /// This action returns a confirmation page for deleting an existing user.
        /// </summary>
        /// <param name="id">The id of the user to be deleted.</param>
        /// <param name="model">The user view model.</param>
        /// <returns>Returns a delete confirmation view.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id, UserViewModel model)
        {
            if (!await this.usersService.ExistsAsync(id.ToString()))
            {
                return base.ShowError("An error occurred", "The user you are trying to delete cannot be found.");
            }

            if (id != model.Id)
            {
                return base.ShowError("An error occurred", "Something went wrong.");
            }

            return View(model);
        }

        /// <summary>
        /// This action handles the delete request.
        /// It deletes the user if it exists and redirects to <see cref="UsersController.Index"/>.
        /// </summary>
        /// <param name="id">The id of the user to be deleted.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await this.usersService.ExistsAsync(id.ToString()))
            {
                return base.ShowError("An error occurred", "The user you are trying to delete cannot be found.");
            }

            await this.usersService.DeleteUserWithId(id.ToString());

            return RedirectToAction(Actions.Index, Controllers.Users, new { area = Areas.Administration });
        }
    }
}
