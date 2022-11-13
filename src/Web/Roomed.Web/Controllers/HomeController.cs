namespace Roomed.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;

    using Roomed.Web.ViewModels;

    using static Roomed.Common.ControllersActionsConstants;

    /// <summary>
    /// A MVC controller inheriting <see cref="BaseController"/>.
    /// The home controller is used for generic purposes.
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// This method redirects to the <see cref="ReservationsController.Index"/> action.
        /// </summary>
        /// <returns>Returns <see cref="IActionResult"/>.</returns>
        public IActionResult Index()
        {
            return RedirectToAction(Actions.Index, Controllers.Reservations);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}