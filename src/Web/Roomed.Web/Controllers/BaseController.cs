namespace Roomed.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The base controller is a base class for all controllers in this applications.
    /// It inherits the default ASP MVC <see cref="Controller"/>.
    /// </summary>
    [Authorize]
    public class BaseController : Controller
    {
    }
}
