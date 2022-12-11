// |-----------------------------------------------------------------------------------------------------|
// <copyright file="DetailedProfileViewModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.ViewModels.Profile
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper.Configuration.Annotations;

    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Dtos.Profile;
    using Roomed.Services.Mapping;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.Profile"/> detailed view model.
    /// </summary>
    public class DetailedProfileViewModel : IMapFrom<DetailedProfileDto>
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

        /// <inheritdoc cref="Roomed.Data.Models.Profile.Birthdate"/>
        public DateOnly? Birthdate { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Profile.Gender"/>
        public Gender? Gender { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Profile.Nationality"/>
        public string? Nationality { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Profile.NationalityCode"/>
        [Display(Name = "Nationality code")]
        public string? NationalityCode { get; set; }

        /// <inheritdoc cref="Roomed.Data.Models.Profile.Address"/>
        public string? Address { get; set; }

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
