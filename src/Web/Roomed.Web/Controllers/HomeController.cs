namespace Roomed.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;

    using Roomed.Web.ViewModels;

    using static Roomed.Common.ControllersActionsConstants;

    public class HomeController : BaseController
    {
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