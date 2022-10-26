namespace Roomed.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Roomed.Web.ViewModels.User;

    public class UserController : Controller
    {
        public IActionResult Login()
        {
            var model = new LoginViewModel();

            return View(model);
        }
    }
}
