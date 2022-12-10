namespace Roomed.Web.Controllers
{
    using System.Diagnostics;

    using Ganss.Xss;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;

    using Roomed.Web.ViewModels;

    using static Roomed.Common.AreasControllersActionsConstants;

    /// <summary>
    /// A MVC controller inheriting <see cref="BaseController"/>.
    /// The home controller is used for generic purposes.
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="sanitizer">The global html sanitizer.</param>
        public HomeController(IHtmlSanitizer sanitizer)
            : base(sanitizer)
        {
        }

        /// <summary>
        /// This action redirects to the <see cref="ReservationsController.Index"/> action.
        /// </summary>
        /// <returns>Returns <see cref="IActionResult"/>.</returns>
        public IActionResult Index()
        {
            return RedirectToAction(Actions.Index, Controllers.Reservations, new { area = string.Empty });
        }

        /// <summary>
        /// This action is used to show the "not implemented" page.
        /// </summary>
        /// <returns>Returns the "not implemented" view.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult NotImplemented()
        {
            return View();
        }

        /// <summary>
        /// This action handles uncaught exceptions by returning a corresponding page.
        /// There are three possible scenarios:
        /// <para> 1. The error is a http status code error.</para>
        /// <para> 2. The error is a custom application error.</para>
        /// <para> 3. The error is neither a http status code error or a custom application error.</para>
        /// </summary>
        /// <param name="statusCode">The http status code.</param>
        /// <returns>Returns a view corresponding to the error.</returns>
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode)
        {
            var model = new ErrorViewModel()
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };

            if (statusCode == null)
            {
                // Catch and handle custom errors.
                var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                var error = exceptionHandlerPathFeature?.Error;

                var errorTitle = TempData["ErrorTitle"]?.ToString();
                var errorMessage = TempData["ErrorMessage"]?.ToString();

                model.ShowRequestId = string.IsNullOrWhiteSpace(model.RequestId);
                model.Title = errorTitle ?? "Something went wrong.";
                model.Message = errorMessage ?? "An error occurred while processing your request. We are sorry for that.";
            }
            else
            {
                // Catch and handle status code errors
                model.ShowRequestId = false;
                model.StatusCode = statusCode;

                switch (statusCode)
                {
                    case 400:
                        model.Title = "Bad request";
                        model.Message = "Your request cannot be processed. Please check the format and syntax and try again.";
                        break;
                    case 403:
                        model.Title = "Forbidden";
                        model.Message = "You are not authorized for this request.";
                        break;
                    case 404:
                        model.Title = "Not found";
                        model.Message = "The resource that you are looking for cannot be found.";
                        break;
                    case 410:
                        model.Title = "Gone";
                        model.Message = "The resource is no longer available.";
                        break;
                    case 418:
                        model.Title = "I'm a teapot";
                        model.Message = "The server says \"I'm a tea pot. I can't brew you any coffee.\".";
                        break;
                    case 429:
                        model.Title = "Too Many Requests";
                        model.Message = "You have sent too many request.Please wait and try again.";
                        break;
                    case 500:
                        model.Title = "Internal Server Error";
                        model.Message = "An error occurred while processing your request. We are sorry for that.";
                        model.ShowRequestId = string.IsNullOrWhiteSpace(model.RequestId);
                        break;
                    case 503:
                        model.Title = "Service Unavailable";
                        model.Message = "The service is not implemented already.";
                        break;
                    case 504:
                        model.Title = "Gateway Timeout";
                        model.Message = "Timeout while receiving upstream response.";
                        break;
                    default:
                        model.Title = "Something went wrong.";
                        model.Message = "An error occurred while processing your request. We are sorry for that.";
                        model.ShowRequestId = string.IsNullOrWhiteSpace(model.RequestId);
                        break;
                }
            }

            return View(model);
        }
    }
}