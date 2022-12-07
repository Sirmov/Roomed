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

    using static Roomed.Common.AreasControllersActionsConstants;
    using static Roomed.Common.GlobalConstants;

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
        /// <param name="mapper">The global auto mapper.</param>
        public ProfilesController(IProfilesService profilesService, IMapper mapper, IHtmlSanitizer sanitizer)
            : base(sanitizer)
        {
            this.profilesService = profilesService;
            this.mapper = mapper;
        }

        /// <summary>
        /// This method returns a view with all profiles.
        /// </summary>
        /// <returns>Returns a task of <see cref="IActionResult"/>.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var profiles = await this.profilesService.GetAllAsync();
            var model = profiles.Select(p => this.mapper.Map<DetailedProfileViewModel>(p));

            return View(model);
        }

        /// <summary>
        /// This method returns a view with a form for creating a new guest profile.
        /// </summary>
        /// <returns>Returns a task of <see cref="IActionResult"/>.</returns>
        [HttpGet]
        public async Task<IActionResult> Create(string? returnUrl)
        {
            var model = new DetailedProfileInputModel();
            ViewBag.ReturnUrl = returnUrl;

            return View(model);
        }

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
                //To do: Uncomment when in production
                //Url.IsLocalUrl(returnUrl) &&
                !string.IsNullOrEmpty(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return RedirectToAction(Actions.Details, new { id = id.ToString() });
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var profile = await this.profilesService.GetAsync(id);
            var model = this.mapper.Map<DetailedProfileViewModel>(profile);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var profile = await this.profilesService.GetAsync(id);
            var model = this.mapper.Map<DetailedProfileInputModel>(profile);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, DetailedProfileInputModel model)
        {
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

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id, ProfileViewModel profile)
        {
            if (id == profile.Id)
            {
                return View(profile);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.profilesService.DeleteAsync(id);
            return RedirectToAction(Actions.Index);
        }

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
