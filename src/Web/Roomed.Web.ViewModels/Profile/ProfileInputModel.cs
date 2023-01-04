// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ProfileInputModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.ViewModels.Profile
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Common.Attribues;

    using static Roomed.Common.Constants.DataConstants.Profile;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.Profile"/> input model.
    /// </summary>
    public class ProfileInputModel
    {
        /// <inheritdoc cref="Roomed.Data.Models.Profile.FirstName"/>
        [Required(AllowEmptyStrings = false)]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        [Display(Name = "First name")]
        [Sanitize]
        public string FirstName { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.Profile.LastName"/>
        [Required(AllowEmptyStrings = false)]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        [Display(Name = "Last name")]
        [Sanitize]
        public string LastName { get; set; } = null!;
    }
}
