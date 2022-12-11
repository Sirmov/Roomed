// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ProfileViewModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.ViewModels.Profile
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper.Configuration.Annotations;

    using Roomed.Services.Mapping;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.Profile"/> view model.
    /// </summary>
    public class ProfileViewModel : IMapFrom<DetailedProfileViewModel>
    {
        /// <inheritdoc cref="Roomed.Data.Common.Models.BaseModel{TKey}.Id"/>
        public Guid Id { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Profile.FirstName"/>
        [Display(Name = "First name")]
        public string FirstName { get; set; } = null!;

        /// <inheritdoc cref="Roomed.Data.Models.Profile.MiddleName"/>
        [Display(Name = "Middle name")]
        public string? MiddleName { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Profile.LastName"/>
        [Display(Name = "Last name")]
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Gets the full name of the guest.
        /// </summary>
        [Ignore]
        public string FullName
        {
            get => $"{this.FirstName} {(this.MiddleName == null ? string.Empty : $"{this.MiddleName} ")}{this.LastName}";
        }
    }
}
