namespace Roomed.Web.Controllers
{
    using AutoMapper;
    using Ganss.Xss;
    using Microsoft.AspNetCore.Mvc;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Contracts;
    using Roomed.Web.ViewModels.IdentityDocument;

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
            ViewBag.Profiles = await this.profilesService.GetAllAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityDocumentInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return Ok(model);
        }
    }
}
