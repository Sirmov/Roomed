namespace Roomed.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using Roomed.Services.Data.Contracts;
    using Roomed.Web.ViewModels.Reservation;

    public class ReservationsController : Controller
    {
        private readonly IReservationsService reservationsService;
        private readonly IMapper mapper;

        public ReservationsController(IReservationsService reservationsService, IMapper mapper)
        {
            this.reservationsService = reservationsService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            // For testing purposes
            //var currentDate = DateOnly.FromDateTime(DateTime.Now);
            var currentDate = DateOnly.FromDateTime(new DateTime(2022, 12, 12));
            var reservations = await this.reservationsService
                .GetAllArrivingFromDateAsync(currentDate);
            var model = reservations.Select(r => mapper.Map<ReservationViewModel>(r));

            return View(model);
        }
    }
}
