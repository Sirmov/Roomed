namespace Roomed.Web.Controllers
{
    using System.Diagnostics;

    using Ganss.Xss;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Roomed.Web.ViewModels;

    using static Roomed.Common.AreasControllersActionsConstants;

    /// <summary>
    /// A MVC controller inheriting <see cref="BaseController"/>.
    /// The home controller is used for generic purposes.
    /// </summary>
    public class HomeController : BaseController
    {
        public HomeController(IHtmlSanitizer sanitizer)
            : base(sanitizer)
        {
        }

        /// <summary>
        /// This method redirects to the <see cref="ReservationsController.Index"/> action.
        /// </summary>
        /// <returns>Returns <see cref="IActionResult"/>.</returns>
        public IActionResult Index()
        {
            return RedirectToAction(Actions.Index, Controllers.Reservations, new { area = string.Empty });
        }

        /// <summary>
        /// This method returns the not implemented page.
        /// </summary>
        /// <returns>Returns <see cref="IActionResult"/>.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult NotImplemented()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}