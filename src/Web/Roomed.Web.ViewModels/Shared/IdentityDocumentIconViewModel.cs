// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IdentityDocumentIconViewModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.ViewModels.Shared
{
    using Roomed.Data.Models.Enums;

    /// <summary>
    /// This is a view model used in the identity document icon partial view.
    /// </summary>
    public class IdentityDocumentIconViewModel
    {
        /// <summary>
        /// Gets or sets the type of the identity document.
        /// </summary>
        public IdentityDocumentType? Type { get; set; }

        /// <summary>
        /// Gets or sets the width of the icon.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the icon.
        /// </summary>
        public int Height { get; set; }
    }
}
