// |-----------------------------------------------------------------------------------------------------|
// <copyright file="BaseController.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.Areas.Administration.Controllers
{
    using System.Reflection;

    using Ganss.Xss;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Roomed.Common.Attribues;
    using Roomed.Common.Constants;

    using static Roomed.Common.Constants.AreasControllersActionsConstants;

    /// <summary>
    /// The base controller is a base class for all controllers in the administration area.
    /// It inherits the default ASP MVC <see cref="Controller"/>.
    /// </summary>
    [Area(Areas.Administration)]
    [Authorize(Policy = "Administration")]
    public class BaseController : Controller
    {
        /// <summary>
        /// A field containing an implementation of the <see cref="IHtmlSanitizer"/>.
        /// </summary>
        protected IHtmlSanitizer sanitizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="sanitizer">The implementation of <see cref="IHtmlSanitizer"/>.</param>
        public BaseController(IHtmlSanitizer sanitizer)
        {
            this.sanitizer = sanitizer;
        }

        /// <summary>
        /// This method sanitizes the properties on a specified model given the <see cref="SanitizeAttribute"/>.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model to be sanitized.</param>
        /// <exception cref="ArgumentNullException">Throws when the model is null.</exception>
        [NonAction]
        protected void SanitizeModel<TModel>(TModel model)
        {
            ArgumentNullException.ThrowIfNull(model, nameof(model));

            var type = model.GetType();

            var flags = BindingFlags.Public | BindingFlags.Instance;

            var properties = type.GetProperties(flags);

            foreach (var property in properties)
            {
                bool shouldBeSanitized = property.IsDefined(typeof(SanitizeAttribute), false);

                if (property.PropertyType == typeof(string) && shouldBeSanitized)
                {
                    string? value = property.GetValue(model) as string;

                    if (value != null)
                    {
                        property.SetValue(model, this.sanitizer.Sanitize(value));
                    }
                }
            }
        }

        /// <summary>
        /// This method redirect to the error view providing an error title and message.
        /// </summary>
        /// <param name="title">The title of the error.</param>
        /// <param name="message">The message of the error.</param>
        /// <returns>Returns <see cref="IActionResult"/>.</returns>
        [NonAction]
        protected IActionResult ShowError(string title, string message)
        {
            TempData[DataKeyConstants.ErrorTitle] = title;
            TempData[DataKeyConstants.ErrorMessage] = message;

            return RedirectToAction(Actions.Error, Controllers.Home, new { area = string.Empty });
        }
    }
}
