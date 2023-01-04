// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ViewScriptTagHelper.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Roomed.Common.Constants;

    /// <summary>
    /// This tag helper is used to create a script tag linking a javascript file according to a specified view.
    /// It automatically appends a file version.
    /// The search paths complies with the standard ASP.NET Core conventions.
    /// For more concrete examples see <see cref="Process(Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext, Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput)"/> documentation.
    /// </summary>
    public class ViewScriptTagHelper : TagHelper
    {
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly IFileVersionProvider fileVersionProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewScriptTagHelper"/> class.
        /// Uses inversion of control to satisfy dependencies.
        /// </summary>
        /// <param name="actionContextAccessor">The implementation of <see cref="IActionContextAccessor"/>.</param>
        /// <param name="fileVersionProvider">The implementation of <see cref="IFileVersionProvider"/>.</param>
        public ViewScriptTagHelper(IActionContextAccessor actionContextAccessor, IFileVersionProvider fileVersionProvider)
        {
            this.actionContextAccessor = actionContextAccessor;
            this.fileVersionProvider = fileVersionProvider;
        }

        /// <summary>
        /// Gets or sets the name of the view.
        /// </summary>
        public string View { get; set; } = null!;

        /// <summary>
        /// Gets or sets the name of the controller.
        /// </summary>
        public string? Controller { get; set; }

        /// <summary>
        /// Gets or sets the name of the area.
        /// </summary>
        public string? Area { get; set; }

        /// <summary>
        /// This method finds a directory path based on the provided tag attributes
        /// and if the script file exists, adds it with the script tag and appends a file version.
        /// All search paths are listed below.
        /// Providing different parameters determines which one to be used.
        /// <para>
        /// <c>/wwwroot/js/areas/{<see langword="areaName"/>}/controllers/{<see langword="controllerName"/>}/{<see langword="viewName"/>}.js</c>.
        /// </para>
        /// <para>
        /// <c>/wwwroot/js/controllers/{<see langword="controllerName"/>}/{<see langword="viewName"/>}.js</c>.
        /// </para>
        /// If <see langword="controllerName"/> is not specified the current controller is assumed.
        /// <see langword="areaName"/>, <see langword="controllerName"/>, <see langword="viewName"/> correspond to the
        /// <see cref="Area"/>, <see cref="Controller"/> and <see cref="View"/> properties.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            this.Controller ??= this?.actionContextAccessor?.ActionContext?.RouteData?.Values["controller"]?.ToString()
                ?? throw new InvalidOperationException(ErrorMessagesConstants.ControlledCanNotBeAssuemd);

            this.View = this.ConvertToCamelCase(this.View) !;
            this.Controller = this.ConvertToCamelCase(this.Controller) !;
            this.Area = this.ConvertToCamelCase(this.Area);

            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\", "wwwroot", "js");

            string path = this.Area == null
                ? Path.Combine(basePath, "controllers", this.Controller, $"{this.View}.js")
                : Path.Combine(basePath, "areas", this.Area, "controllers", this.Controller, $"{this.View}.js");

            path = Path.GetFullPath(path);

            string directory = this.Area == null
                ? @$"/js/controllers/{this.Controller}/{this.View}.js"
                : @$"/js/areas/{this.Area}/controllers/{this.Controller}/{this.View}.js";

            if (!File.Exists(path))
            {
                throw new InvalidOperationException(string.Format(ErrorMessagesConstants.ScriptNotFound, directory));
            }

            output.TagName = "script";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Clear();
            output.Attributes.Add("src", this.fileVersionProvider.AddFileVersionToPath(string.Empty, directory));
        }

        private string? ConvertToCamelCase(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return char.ToLowerInvariant(value[0]) + value.Substring(1);
        }
    }
}
