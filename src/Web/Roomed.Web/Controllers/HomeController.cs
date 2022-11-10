namespace Roomed.Web.Controllers
{
    using System.Diagnostics;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Roomed.Data.Models;
    using Roomed.Services.Data.Contracts;
    using Roomed.Web.ViewModels;
    using Roomed.Web.ViewModels.Reservation;

    public class HomeController : BaseController
    {
        private readonly IReservationsService reservationsService;
        private readonly IMapper mapper;

        public HomeController(IReservationsService reservationsService, IMapper mapper)
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}