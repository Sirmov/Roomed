// |-----------------------------------------------------------------------------------------------------|
// <copyright file="HomeController.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.Areas.Administration.Controllers
{
    using Ganss.Xss;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// A MVC controller inheriting <see cref="BaseController"/>.
    /// The home controller is used for viewing the home and not implemented page.
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
        /// This method returns the administration home page.
        /// </summary>
        /// <returns>Returns <see cref="IActionResult"/>.</returns>
        public IActionResult Index()
        {
            return View();
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
    }
}
