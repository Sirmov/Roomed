// |-----------------------------------------------------------------------------------------------------|
// <copyright file="DetailedProfileViewModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.ViewModels.Profile
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Dtos.Profile;
    using Roomed.Services.Mapping;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.Profile"/> detailed view model.
    /// </summary>
    public class DetailedProfileViewModel : ProfileViewModel, IMapFrom<DetailedProfileDto>
    {
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
    }
}
