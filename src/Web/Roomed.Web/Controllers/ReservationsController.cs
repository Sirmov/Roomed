namespace Roomed.Web.Controllers
{
    using AutoMapper;
    using Ganss.Xss;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Roomed.Data.Models;
    using Roomed.Services.Data.Contracts;
    using Roomed.Web.ViewModels.Profile;
    using Roomed.Web.ViewModels.Reservation;
    using Roomed.Web.ViewModels.RoomType;

    using static Roomed.Common.AreasControllersActionsConstants;

    /// <summary>
    /// A MVC controller inheriting <see cref="BaseController"/>.
    /// The reservations controller is responsible for all operations regarding the <see cref="Reservation"/> model.
    /// </summary>
    public class ReservationsController : BaseController
    {
        private readonly IReservationsService reservationsService;
        private readonly IProfilesService profilesService;
        private readonly IRoomTypesService roomTypesService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationsController"/> class.
        /// Uses constructor injection to resolve dependencies.
        /// </summary>
        /// <param name="reservationsService">The implementation of <see cref="IReservationsService"/>.</param>
        /// <param name="profilesService">The implementation of <see cref="IProfilesService"/>.</param>
        /// <param name="roomTypesService">The implementation of <see cref="IRoomTypesService"/>.</param>
        /// <param name="mapper">The global auto mapper.</param>
        /// <param name="sanitizer">The global html sanitizer.</param>
        public ReservationsController(
            IReservationsService reservationsService,
            IProfilesService profilesService,
            IRoomTypesService roomTypesService,
            IMapper mapper,
            IHtmlSanitizer sanitizer)
            : base(sanitizer)
        {
            this.reservationsService = reservationsService;
            this.mapper = mapper;
            this.profilesService = profilesService;
            this.roomTypesService = roomTypesService;
        }

        /// <summary>
        /// This method returns a view with a table of the arriving today reservations.
        /// </summary>
        /// <returns>Returns a task of <see cref="IActionResult"/>.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // For testing purposes
            // var currentDate = DateOnly.FromDateTime(DateTime.Now);
            var currentDate = DateOnly.FromDateTime(new DateTime(2022, 12, 12));
            var reservations = await this.reservationsService.GetAllArrivingFromDateAsync(currentDate);
            var model = reservations.Select(r => mapper.Map<ReservationViewModel>(r));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new ReservationInputModel();
            var profiles = await this.profilesService.GetAllAsync();
            var roomTypes = (await this.roomTypesService.GetAllAsync())
                .Select(rt => this.mapper.Map<RoomTypeViewModel>(rt));

            ViewBag.Profiles = profiles.Select(p => this.mapper.Map<DetailedProfileViewModel>(p));
            ViewBag.RoomTypesSelectList = new SelectList(
                    roomTypes.Select(rt => new SelectListItem() { Value = rt.Id.ToString(), Text = rt.Name }),
                    "Value",
                    "Text");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationInputModel model)
        {
            await this.ValidateReservation(ModelState, model);

            if (!ModelState.IsValid)
            {
                var profiles = await this.profilesService.GetAllAsync();
                var roomTypes = (await this.roomTypesService.GetAllAsync())
                    .Select(rt => this.mapper.Map<RoomTypeViewModel>(rt));

                ViewBag.Profiles = profiles.Select(p => this.mapper.Map<DetailedProfileViewModel>(p));
                ViewBag.RoomTypesSelectList = new SelectList(
                        roomTypes.Select(rt => new SelectListItem() { Value = rt.Id.ToString(), Text = rt.Name }),
                        "Value",
                        "Text");

                return View(model);
            }

            return RedirectToAction(Actions.ChooseRoom, Controllers.Reservations, new { model = model });
        }

        private async Task ValidateReservation(ModelStateDictionary modelState, ReservationInputModel model)
        {
            if (modelState.IsValid == false)
            {
                return;
            }

            if (!await this.profilesService.ExistsAsync(model.ReservationHolderId))
            {
                modelState.AddModelError(nameof(model.ReservationHolderId), "Guest profile does not exist.");
            }

            if (!await this.roomTypesService.ExistsAsync(model.RoomTypeId))
            {
                modelState.AddModelError(nameof(model.RoomTypeId), "Room type does not exist.");
            }

            var today = DateOnly.FromDateTime(DateTime.Now);

            if (model.ArrivalDate < today)
            {
                ModelState.AddModelError(nameof(model.ArrivalDate), "Can not create a reservation from the past.");
            }

            if (model.Adults < 1)
            {
                ModelState.AddModelError(nameof(model.Adults), "There should be at least one adult in a room.");
            }
        }
    }
}
