namespace Roomed.Web.Controllers
{
    using AutoMapper;
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
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityDocumentsController"/> class.
        /// Uses constructor injection to resolve dependencies.
        /// </summary>
        /// <param name="identityDocumentsService">The implementation of <see cref="IIdentityDocumentsService"/>.</param>
        /// <param name="mapper">The global auto mapper.</param>
        public IdentityDocumentsController(IIdentityDocumentsService identityDocumentsService, IMapper mapper)
        {
            this.identityDocumentsService = identityDocumentsService;
            this.mapper = mapper;
        }

        /// <summary>
        /// This method returns a view with all identity documents.
        /// </summary>
        /// <returns>Returns a task of <see cref="IActionResult"/>.</returns>
        public async Task<IActionResult> Index()
        {
            var documents = await this.identityDocumentsService.GetAllAsync();
            var model = documents.Select(id => mapper.Map<IdentityDocumentViewModel>(id));

            return View(model);
        }
    }
}
