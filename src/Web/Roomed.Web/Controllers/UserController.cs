namespace Roomed.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class UserController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
