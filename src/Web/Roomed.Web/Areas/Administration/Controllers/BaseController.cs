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

    using static Roomed.Common.AreasControllersActionsConstants;

    /// <summary>
    /// The base controller is a base class for all controllers in the administration area.
    /// It inherits the default ASP MVC <see cref="Controller"/>.
    /// </summary>
    [Area(Areas.Administration)]
    [Authorize(Policy = "Administration")]
    public class BaseController : Controller
    {
        protected IHtmlSanitizer sanitizer;

        public BaseController(IHtmlSanitizer sanitizer)
        {
            this.sanitizer = sanitizer;
        }

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
    }
}
