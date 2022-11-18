namespace Roomed.Web.Controllers
{
    using System.Globalization;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using Roomed.Services.Data.Contracts;
    using Roomed.Web.ViewModels.Profile;

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
        public ProfilesController(IProfilesService profilesService, IMapper mapper)
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
            var model = profiles.Select(p => this.mapper.Map<ProfileViewModel>(p));

            return View(model);
        }

        /// <summary>
        /// This method returns a view with a form for creating a new guest profile.
        /// </summary>
        /// <returns>Returns a task of <see cref="IActionResult"/>.</returns>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new DetailedProfileInputModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DetailedProfileInputModel model)
        {
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

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return Ok(model);
        }
    }
}
