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
    using static Roomed.Common.DataConstants;

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
        /// <param name="mapper">The global auto mapper.</param>
        /// <param name="sanitizer">The global html sanitizer.</param>
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

            TempData["CreateReservationModel"] = JsonConvert.SerializeObject(model);
            return RedirectToAction(Actions.ChooseRoom, Controllers.Reservations);
        }

        [HttpGet]
        public async Task<IActionResult> ChooseRoom()
        {
            var json = TempData["CreateReservationModel"]?.ToString();

            if (json == null)
            {
                return RedirectToAction(Actions.Create, Controllers.Reservations);
            }

            var model = JsonConvert.DeserializeObject<ReservationInputModel>(json);
            var roomType = await this.roomTypesService.GetAsync(model.RoomTypeId);

            var rooms = await this.roomsService.GetAllFreeRoomsAsync(model.ArrivalDate, model.DepartureDate, roomType);

            ViewData["FreeRooms"] = rooms.Select(r => this.mapper.Map<RoomViewModel>(r));
            ViewData["ReservationInputModel"] = model;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChooseRoom(int roomId, ReservationInputModel model)
        {
            var roomType = await this.roomTypesService.GetAsync(model.RoomTypeId);
            var freeRooms = await this.roomsService.GetAllFreeRoomsAsync(model.ArrivalDate, model.DepartureDate, roomType);

            if (!freeRooms.Any(r => r.Id == roomId))
            {
                return BadRequest();
            }

            var dto = this.mapper.Map<ReservationDto>(model);

            Guid id;

            try
            {
                id = await this.reservationsService.CreateReservationAsync(dto, roomId);
            }
            catch (Exception exception)
            {
                return BadRequest();
                throw;
            }

            return RedirectToAction(Actions.Index);
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
