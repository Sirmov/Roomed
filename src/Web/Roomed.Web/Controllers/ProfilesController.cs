// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ProfilesController.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.Controllers
{
    using System.Globalization;

    using AutoMapper;
    using Ganss.Xss;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.Profile;
    using Roomed.Web.ViewModels.Profile;

    using static Roomed.Common.Constants.AreasControllersActionsConstants;
    using static Roomed.Common.Constants.GlobalConstants;

    using Profile = Roomed.Data.Models.Profile;

    /// <summary>
    /// A MVC controller inheriting <see cref="BaseController"/>.
    /// The profiles controller is responsible for all operations regarding the <see cref="Profile"/> model.
    /// </summary>
    public class ProfilesController : BaseController
    {
        private readonly IProfilesService profilesService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilesController"/> class.
        /// Uses constructor injection to resolve dependencies.
        /// </summary>
        /// <param name="profilesService">The implementation of <see cref="IProfilesService"/>.</param>
        /// <param name="mapper">The implementation of <see cref="IMapper"/>.</param>
        /// <param name="sanitizer">The implementation of <see cref="IHtmlSanitizer"/>.</param>
        public ProfilesController(IProfilesService profilesService, IMapper mapper, IHtmlSanitizer sanitizer)
            : base(sanitizer)
        {
            this.profilesService = profilesService;
            this.mapper = mapper;
        }

        /// <summary>
        /// This action returns a page with all profiles.
        /// </summary>
        /// <returns>Returns a view with all profiles.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var profiles = await this.profilesService.GetAllAsync();
            var model = profiles.Select(p => this.mapper.Map<DetailedProfileViewModel>(p));

            return View(model);
        }

        /// <summary>
        /// This action returns a page with a form for creating a new guest profile.
        /// </summary>
        /// <param name="returnUrl">Optional return url.
        /// <para>
        /// Exp: Used when creating a reservation but the guest does not have a profile
        /// and one should be created. When the creation is successful the user is redirected
        /// back to continue the making of the reservation.
        /// </para>
        /// </param>
        /// <returns>Returns the create guest profile view.</returns>
        [HttpGet]
        public IActionResult Create(string? returnUrl)
        {
            var model = new DetailedProfileInputModel();
            ViewBag.ReturnUrl = returnUrl;

            return View(model);
        }

        /// <summary>
        /// This action handles the create request.
        /// It validates the model, sanitizes it and adds it to the database.
        /// Returns the same view if the validation fails otherwise redirects to <see cref="ProfilesController.Details(Guid)"/> action
        /// or to the <paramref name="returnUrl"/> if it is not null.
        /// </summary>
        /// <param name="returnUrl">Optional return url.
        /// <para>
        /// Exp: Used when creating a reservation but the guest does not have a profile
        /// and one should be created. When the creation is successful the user is redirected
        /// back to continue the making of the reservation.
        /// </para>
        /// </param>
        /// <param name="model">The profile input model.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(string? returnUrl, DetailedProfileInputModel model)
        {
            this.ValidateProfile(ModelState, model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.SanitizeModel(model);
            var dto = this.mapper.Map<DetailedProfileDto>(model);
            var id = await this.profilesService.CreateDetailedAsync(dto);

            if (returnUrl != null &&

                // To do: Uncomment when in production
                // Url.IsLocalUrl(returnUrl) &&
                !string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(Actions.Details, new { id = id.ToString() });
        }

        /// <summary>
        /// This action returns a page with details of an existing guest profile.
        /// </summary>
        /// <param name="id">The id of the guest profile.</param>
        /// <returns>Returns the details guest profile view.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            if (!await this.profilesService.ExistsAsync(id))
            {
                return base.ShowError("An error occurred", "The guest profile you are trying to view cannot be found.");
            }

            var profile = await this.profilesService.GetAsync(id);
            var model = this.mapper.Map<DetailedProfileViewModel>(profile);

            return View(model);
        }

        /// <summary>
        /// This action returns a page with a form for editing an existing guest profile.
        /// </summary>
        /// <param name="id">The id of the guest profile to be edited.</param>
        /// <returns>Returns the edit guest profile view.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (!await this.profilesService.ExistsAsync(id))
            {
                return base.ShowError("An error occurred", "The guest profile you are trying to edit cannot be found.");
            }

            var profile = await this.profilesService.GetAsync(id);
            var model = this.mapper.Map<DetailedProfileInputModel>(profile);

            return View(model);
        }

        /// <summary>
        /// This action handles the edit request.
        /// It validates the model, sanitizes it and modifies the existing entity.
        /// Returns the same view if the validation fails otherwise redirects to <see cref="ProfilesController.Details(Guid)"/> action.
        /// </summary>
        /// <param name="id">The id of the guest profile to be edited.</param>
        /// <param name="model">The guest profile input model.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, DetailedProfileInputModel model)
        {
            if (!await this.profilesService.ExistsAsync(id))
            {
                return base.ShowError("An error occurred", "The guest profile you are trying to edit cannot be found.");
            }

            this.ValidateProfile(ModelState, model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.SanitizeModel(model);
            var dto = this.mapper.Map<DetailedProfileDto>(model);
            await this.profilesService.EditAsync(id, dto);

            return RedirectToAction(Actions.Details, new { id = id.ToString() });
        }

        /// <summary>
        /// This action returns a confirmation page for deleting an existing guest profile.
        /// </summary>
        /// <param name="id">The id of the guest profile to be deleted.</param>
        /// <param name="profile">The view model of the guest profile.</param>
        /// <returns>Returns a delete confirmation view.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id, ProfileViewModel profile)
        {
            if (!await this.profilesService.ExistsAsync(id))
            {
                return base.ShowError("An error occurred", "The guest profile you are trying to delete cannot be found.");
            }

            if (id != profile.Id)
            {
                return base.ShowError("An error occurred", "Something went wrong.");
            }

            return View(profile);
        }

        /// <summary>
        /// This action handles the delete request.
        /// It deletes the guest profile if it exists and redirects to <see cref="ProfilesController.Index"/>.
        /// </summary>
        /// <param name="id">The id of the guest profile to be deleted.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await this.profilesService.ExistsAsync(id))
            {
                return base.ShowError("An error occurred", "The guest profile you are trying to delete cannot be found.");
            }

            await this.profilesService.DeleteAsync(id);
            return RedirectToAction(Actions.Index);
        }

        [NonAction]
        private void ValidateProfile(ModelStateDictionary modelState, DetailedProfileInputModel model)
        {
            if (modelState.IsValid == false)
            {
                return;
            }

            if (!NationalitiesDictionary.ContainsKey(model.Nationality))
            {
                ModelState.AddModelError(nameof(model.Nationality), "Invalid nationality.");
            }
            else
            {
                if (NationalitiesDictionary[model.Nationality] != model.NationalityCode)
                {
                    ModelState.AddModelError(nameof(model.Nationality), "Nationality not equal to nationality code.");
                    ModelState.AddModelError(nameof(model.NationalityCode), "Nationality code not equal to nationality.");
                }
            }

            if (!NationalityCodes.Contains(model.NationalityCode))
            {
                ModelState.AddModelError(nameof(model.NationalityCode), "Invalid nationality code.");
            }

            var today = DateOnly.FromDateTime(DateTime.Now);

            if (model.Birthdate >= today)
            {
                ModelState.AddModelError(nameof(model.Birthdate), $"Birthday should be before {today.ToString(CultureInfo.InvariantCulture)}.");
            }

            var before120Years = today.AddYears(-120);

            if (model.Birthdate <= before120Years)
            {
                ModelState.AddModelError(nameof(model.Birthdate), $"Please enter a valid birthdate.");
            }
        }
    }
}
