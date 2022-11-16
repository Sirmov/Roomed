namespace Roomed.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Contracts;
    using Roomed.Services.Data.Dtos.Reservation;
    using Roomed.Web.ViewModels.Reservation;

    /// <summary>
    /// A MVC controller inheriting <see cref="BaseController"/>.
    /// The reservations controller is responsible for all operations regarding the <see cref="Reservation"/> model.
    /// </summary>
    public class ReservationsController : BaseController
    {
        private readonly IReservationsService reservationsService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationsController"/> class.
        /// Uses constructor injection to resolve dependencies.
        /// </summary>
        /// <param name="reservationsService">The implementation of <see cref="IReservationsService"/>.</param>
        /// <param name="mapper">The global auto mapper.</param>
        public ReservationsController(IReservationsService reservationsService, IMapper mapper)
        {
            this.reservationsService = reservationsService;
            this.mapper = mapper;
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
    }
}
