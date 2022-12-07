namespace Roomed.Web.Controllers
{
    using System;
    using System.Reflection;

    using Ganss.Xss;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Roomed.Common.Attribues;

    /// <summary>
    /// The base controller is a base class for all controllers in this application.
    /// It inherits the default ASP MVC <see cref="Controller"/>.
    /// </summary>
    [Authorize(Policy = "FrontOffice")]
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
