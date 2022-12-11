// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ErrorViewModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.ViewModels
{
    /// <summary>
    /// This class is a view model for displaying errors.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets the id of the request.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the request id should be shown.
        /// </summary>
        public bool ShowRequestId { get; set; }

        /// <summary>
        /// Gets or sets the message of the error.
        /// </summary>
        public string Message { get; set; } = null!;

        /// <summary>
        /// Gets or sets the title of the error.
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        /// Gets or sets the HTTP status code of the error.
        /// </summary>
        public int? StatusCode { get; set; }
    }
}