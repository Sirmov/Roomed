namespace Roomed.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using Roomed.Services.Data.Contracts;
    using Roomed.Web.ViewModels.IdentityDocument;

    public class IdentityDocumentsController : BaseController
    {
        private readonly IIdentityDocumentsService identityDocumentsService;
        private readonly IMapper mapper;

        public IdentityDocumentsController(IIdentityDocumentsService identityDocumentsService, IMapper mapper)
        {
            this.identityDocumentsService = identityDocumentsService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var documents = await this.identityDocumentsService
                .GetAllAsync();
            var model = documents.Select(id => mapper.Map<IdentityDocumentViewModel>(id));

            return View(model);
        }
    }
}
