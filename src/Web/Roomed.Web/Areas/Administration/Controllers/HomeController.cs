namespace Roomed.Web.Areas.Administration.Controllers
{
    using Ganss.Xss;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        public HomeController(IHtmlSanitizer sanitizer)
            : base(sanitizer)
        {
        }

        /// <summary>
        /// This method returns the administration home page.
        /// </summary>
        /// <returns>Returns <see cref="IActionResult"/>.</returns>
        public IActionResult Index()
        {
            return View();
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
    }
}
