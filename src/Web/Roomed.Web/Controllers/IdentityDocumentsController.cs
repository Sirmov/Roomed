namespace Roomed.Web.Controllers
{
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
        /// <param name="mapper">The global auto mapper.</param>
        /// <param name="sanitizer">The global html sanitizer.</param>
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
        /// This method returns a view with all identity documents.
        /// </summary>
        /// <returns>Returns a task of <see cref="IActionResult"/>.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var documents = await this.identityDocumentsService.GetAllAsync();
            var model = documents.Select(id => mapper.Map<IdentityDocumentViewModel>(id));

            return View(model);
        }

        /// <summary>
        /// This method returns a view with a form for creating a new identity document.
        /// </summary>
        /// <returns>Returns a task of <see cref="IActionResult"/>.</returns>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new IdentityDocumentInputModel();
            var profiles = await this.profilesService.GetAllAsync();
            ViewBag.Profiles = profiles.Select(p => this.mapper.Map<DetailedProfileViewModel>(p));

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityDocumentInputModel model)
        {
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

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var document = await this.identityDocumentsService.GetAsync(id);
            var model = this.mapper.Map<IdentityDocumentViewModel>(document);

            var owner = await this.profilesService.GetAsync(document.OwnerId);
            ViewBag.Owner = this.mapper.Map<DetailedProfileViewModel>(owner);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var document = await this.identityDocumentsService.GetAsync(id);
            var model = this.mapper.Map<IdentityDocumentInputModel>(document);

            var profiles = await this.profilesService.GetAllAsync();
            ViewBag.Profiles = profiles.Select(p => this.mapper.Map<DetailedProfileViewModel>(p));

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, IdentityDocumentInputModel model)
        {
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

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id, IdentityDocumentViewModel identityDocument)
        {
            if (id == identityDocument.Id)
            {
                return View(identityDocument);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.identityDocumentsService.DeleteAsync(id);
            return RedirectToAction(Actions.Index);
        }

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
