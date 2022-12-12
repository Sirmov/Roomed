// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IdentityDocumentsController.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.Controllers
{
    using System;
    using System.Globalization;

    using AutoMapper;
    using Ganss.Xss;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using Roomed.Data.Models;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.IdentityDocument;
    using Roomed.Web.ViewModels.IdentityDocument;
    using Roomed.Web.ViewModels.Profile;

    using static Roomed.Common.AreasControllersActionsConstants;

    /// <summary>
    /// A MVC controller inheriting <see cref="BaseController"/>.
    /// The identity documents controller is responsible for all operations regarding the <see cref="IdentityDocument"/> model.
    /// </summary>
    public class IdentityDocumentsController : BaseController
    {
        private readonly IIdentityDocumentsService identityDocumentsService;
        private readonly IProfilesService profilesService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityDocumentsController"/> class.
        /// Uses constructor injection to resolve dependencies.
        /// </summary>
        /// <param name="identityDocumentsService">The implementation of <see cref="IIdentityDocumentsService"/>.</param>
        /// <param name="profilesService">The implementation of <see cref="IProfilesService"/>.</param>
        /// <param name="mapper">The implementation of <see cref="IMapper"/>.</param>
        /// <param name="sanitizer">The implementation of <see cref="IHtmlSanitizer"/>.</param>
        public IdentityDocumentsController(
            IIdentityDocumentsService identityDocumentsService,
            IProfilesService profilesService,
            IMapper mapper,
            IHtmlSanitizer sanitizer)
            : base(sanitizer)
        {
            this.identityDocumentsService = identityDocumentsService;
            this.profilesService = profilesService;
            this.mapper = mapper;
        }

        /// <summary>
        /// This action returns a page with all identity documents.
        /// </summary>
        /// <returns>Returns a view with all identity documents.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var documents = await this.identityDocumentsService.GetAllAsync();
            var model = documents.Select(id => mapper.Map<IdentityDocumentViewModel>(id));

            return View(model);
        }

        /// <summary>
        /// This action returns a page with a form for creating a new identity document.
        /// </summary>
        /// <returns>Returns the create identity document view.</returns>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new IdentityDocumentInputModel();
            var profiles = await this.profilesService.GetAllAsync();
            ViewBag.Profiles = profiles.Select(p => this.mapper.Map<DetailedProfileViewModel>(p));

            return View(model);
        }

        /// <summary>
        /// This action handles the create request.
        /// It validates the model, sanitizes it and adds it to the database.
        /// Returns the same view if the validation fails otherwise redirects to <see cref="IdentityDocumentsController.Details(Guid)"/> action.
        /// </summary>
        /// <param name="model">The identity document input model.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(IdentityDocumentInputModel model)
        {
            if (!await this.profilesService.ExistsAsync(model.OwnerId))
            {
                return base.ShowError("An error occurred", "The owner of the document you are trying to create cannot be found.");
            }

            await this.ValidateIdentityDocument(ModelState, model);

            if (!ModelState.IsValid)
            {
                var profiles = await this.profilesService.GetAllAsync();
                ViewBag.Profiles = profiles.Select(p => this.mapper.Map<DetailedProfileViewModel>(p));
                return View(model);
            }

            this.SanitizeModel(model);
            var dto = this.mapper.Map<IdentityDocumentDto>(model);
            var id = await this.identityDocumentsService.CreateAsync(dto);

            return RedirectToAction(Actions.Details, new { id = id.ToString() });
        }

        /// <summary>
        /// This actions returns a page with the details of a identity document with a given id.
        /// </summary>
        /// <param name="id">The id of the identity document.</param>
        /// <returns>Returns the identity document details view.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            if (!await this.identityDocumentsService.ExistsAsync(id))
            {
                return base.ShowError("An error occurred", "The identity document you are trying to view cannot be found.");
            }

            var document = await this.identityDocumentsService.GetAsync(id);
            var model = this.mapper.Map<IdentityDocumentViewModel>(document);

            var owner = await this.profilesService.GetAsync(document.OwnerId);
            ViewBag.Owner = this.mapper.Map<DetailedProfileViewModel>(owner);

            return View(model);
        }

        /// <summary>
        /// This action returns a page with a form for editing an existing identity document.
        /// </summary>
        /// <param name="id">The id of the identity document to be edited.</param>
        /// <returns>Returns the edit identity document view.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (!await this.identityDocumentsService.ExistsAsync(id))
            {
                return base.ShowError("An error occurred", "The identity document you are trying to edit cannot be found.");
            }

            var document = await this.identityDocumentsService.GetAsync(id);
            var model = this.mapper.Map<IdentityDocumentInputModel>(document);

            var profiles = await this.profilesService.GetAllAsync();
            ViewBag.Profiles = profiles.Select(p => this.mapper.Map<DetailedProfileViewModel>(p));

            return View(model);
        }

        /// <summary>
        /// This action handles the edit request.
        /// It validates the model, sanitizes it and modifies the existing entity.
        /// Returns the same view if the validation fails otherwise redirects to <see cref="IdentityDocumentsController.Details(Guid)"/> action.
        /// </summary>
        /// <param name="id">The id of the identity document to be edited.</param>
        /// <param name="model">The identity document input model.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, IdentityDocumentInputModel model)
        {
            if (!await this.identityDocumentsService.ExistsAsync(id))
            {
                return base.ShowError("An error occurred", "The identity document you are trying to edit cannot be found.");
            }

            if (!await this.profilesService.ExistsAsync(model.OwnerId))
            {
                return base.ShowError("An error occurred", "The owner of the document you are trying to edit cannot be found.");
            }

            await this.ValidateIdentityDocument(ModelState, model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.SanitizeModel(model);
            var dto = this.mapper.Map<IdentityDocumentDto>(model);
            await this.identityDocumentsService.EditAsync(id, dto);

            return RedirectToAction(Actions.Details, new { id = id.ToString() });
        }

        /// <summary>
        /// This action returns a confirmation page for deleting an existing identity document.
        /// </summary>
        /// <param name="id">The id of the identity document to be deleted.</param>
        /// <param name="identityDocument">The view model of the identity document.</param>
        /// <returns>Returns a delete confirmation view.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id, IdentityDocumentViewModel identityDocument)
        {
            if (!await this.identityDocumentsService.ExistsAsync(id))
            {
                return base.ShowError("An error occurred", "The identity document you are trying to delete cannot be found.");
            }

            if (id != identityDocument.Id)
            {
                return base.ShowError("An error occurred", "Something went wrong.");
            }

            return View(identityDocument);
        }

        /// <summary>
        /// This action handles the delete request.
        /// It deletes the identity document if it exists and redirects to <see cref="IdentityDocumentsController.Index"/>.
        /// </summary>
        /// <param name="id">The id of the identity document to be deleted.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await this.identityDocumentsService.ExistsAsync(id))
            {
                return base.ShowError("An error occurred", "The identity document you are trying to delete cannot be found.");
            }

            await this.identityDocumentsService.DeleteAsync(id);
            return RedirectToAction(Actions.Index);
        }

        [NonAction]
        private async Task ValidateIdentityDocument(ModelStateDictionary modelState, IdentityDocumentInputModel model)
        {
            if (modelState.IsValid == false)
            {
                return;
            }

            if (!await this.profilesService.ExistsAsync(model.OwnerId))
            {
                modelState.AddModelError(nameof(model.OwnerId), "Guest profile does not exist.");
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
