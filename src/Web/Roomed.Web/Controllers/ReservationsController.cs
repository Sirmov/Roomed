// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationsController.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.Controllers
{
    using AutoMapper;
    using Ganss.Xss;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Newtonsoft.Json;

    using Roomed.Data.Models;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.Reservation;
    using Roomed.Web.ViewModels.Profile;
    using Roomed.Web.ViewModels.Reservation;
    using Roomed.Web.ViewModels.Room;
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
        private readonly IRoomsService roomsService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationsController"/> class.
        /// Uses constructor injection to resolve dependencies.
        /// </summary>
        /// <param name="reservationsService">The implementation of <see cref="IReservationsService"/>.</param>
        /// <param name="profilesService">The implementation of <see cref="IProfilesService"/>.</param>
        /// <param name="roomTypesService">The implementation of <see cref="IRoomTypesService"/>.</param>
        /// <param name="roomsService">The implementation of <see cref="IRoomsService"/>.</param>
        /// <param name="mapper">The implementation of <see cref="IMapper"/>.</param>
        /// <param name="sanitizer">The implementation of <see cref="IHtmlSanitizer"/>.</param>
        public ReservationsController(
            IReservationsService reservationsService,
            IProfilesService profilesService,
            IRoomTypesService roomTypesService,
            IRoomsService roomsService,
            IMapper mapper,
            IHtmlSanitizer sanitizer)
            : base(sanitizer)
        {
            this.reservationsService = reservationsService;
            this.mapper = mapper;
            this.profilesService = profilesService;
            this.roomTypesService = roomTypesService;
            this.roomsService = roomsService;
        }

        /// <summary>
        /// This action returns a page with a table of the arriving today reservations.
        /// </summary>
        /// <returns>Returns a view with all arriving today reservations.>.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            var reservations = await this.reservationsService.GetAllArrivingFromDateAsync(currentDate);
            var model = reservations.Select(r => mapper.Map<ReservationViewModel>(r));

            ViewData["ReservationsType"] = "Arriving";
            return View(model);
        }

        /// <summary>
        /// This action returns a page with a table of the in house today reservations.
        /// </summary>
        /// <returns>Returns a view with all in house today reservations.>.</returns>
        [HttpGet]
        public async Task<IActionResult> InHouse()
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            var reservations = await this.reservationsService.GetAllInHouseFromDateAsync(currentDate);
            var model = reservations.Select(r => mapper.Map<ReservationViewModel>(r));

            ViewData["ReservationsType"] = "In House";
            return View("Index", model);
        }

        /// <summary>
        /// This action returns a page with a table of the departing today reservations.
        /// </summary>
        /// <returns>Returns a view with all departing today reservations.>.</returns>
        [HttpGet]
        public async Task<IActionResult> Departing()
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            var reservations = await this.reservationsService.GetAllDepartingFromDateAsync(currentDate);
            var model = reservations.Select(r => mapper.Map<ReservationViewModel>(r));

            ViewData["ReservationsType"] = "Departing";
            return View("Index", model);
        }

        /// <summary>
        /// This action returns a page with a form for creating a new reservation.
        /// </summary>
        /// <returns>Returns the create reservation view.</returns>
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

        /// <summary>
        /// This action handles the create request.
        /// It validates the model and returns the same view if the validation fails
        /// otherwise redirects to <see cref="ReservationsController.ChooseRoom()"/> action.
        /// </summary>
        /// <param name="model">The reservation input model.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="IActionResult"/>.</returns>
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

            TempData["CreateReservationModel"] = JsonConvert.SerializeObject(model);
            return RedirectToAction(Actions.ChooseRoom, Controllers.Reservations);
        }

        /// <summary>
        /// This action returns a page with all compatible free rooms
        /// for the period of the reservation being made.
        /// </summary>
        /// <returns>Returns the choose room view.</returns>
        [HttpGet]
        public async Task<IActionResult> ChooseRoom()
        {
            var json = TempData["CreateReservationModel"]?.ToString();

            if (json == null)
            {
                return RedirectToAction(Actions.Create, Controllers.Reservations);
            }

            var model = JsonConvert.DeserializeObject<ReservationInputModel>(json) !;

            if (!await this.roomTypesService.ExistsAsync(model.RoomTypeId))
            {
                return base.ShowError("An error occurred", "The room type of the reservation cannot be found.");
            }

            var roomType = await this.roomTypesService.GetAsync(model.RoomTypeId);

            var rooms = await this.roomsService.GetAllFreeRoomsAsync(model.ArrivalDate, model.DepartureDate, roomType);

            ViewData["FreeRooms"] = rooms.Select(r => this.mapper.Map<RoomViewModel>(r));
            ViewData["ReservationInputModel"] = model;
            return View(model);
        }

        /// <summary>
        /// This action handles the choose room request.
        /// It takes the reservation input model and the chosen room,
        /// validates that room is actually free and attempts to create the reservation.
        /// </summary>
        /// <param name="roomId">The id of the chosen room.</param>
        /// <param name="model">The reservation input model.</param>
        /// <returns>Returns a <see cref="Task{TResult}"/> of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public async Task<IActionResult> ChooseRoom(int roomId, ReservationInputModel model)
        {
            if (!await this.roomTypesService.ExistsAsync(model.RoomTypeId))
            {
                return base.ShowError("An error occurred", "The room type of the reservation cannot be found.");
            }

            var roomType = await this.roomTypesService.GetAsync(model.RoomTypeId);
            var freeRooms = await this.roomsService.GetAllFreeRoomsAsync(model.ArrivalDate, model.DepartureDate, roomType);

            if (!freeRooms.Any(r => r.Id == roomId))
            {
                return base.ShowError("An error occurred", "The room you have chosen is not free for this period.");
            }

            var dto = this.mapper.Map<ReservationDto>(model);

            Guid id;

            try
            {
                id = await this.reservationsService.CreateReservationAsync(dto, roomId);
            }
            catch (Exception)
            {
                return base.ShowError("An error occurred", "Something went wrong trying to create the reservation. Please try again.");
            }

            return RedirectToAction(Actions.Index);
        }

        [NonAction]
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
