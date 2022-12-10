// <copyright file="ProfileViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Roomed.Web.ViewModels.Profile
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper.Configuration.Annotations;

    using Roomed.Services.Mapping;

    public class ProfileViewModel : IMapFrom<DetailedProfileViewModel>
    {
        public Guid Id { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle name")]
        public string? MiddleName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Ignore]
        public string FullName
        {
            get => $"{this.FirstName} {(this.MiddleName == null ? string.Empty : $"{this.MiddleName} ")}{this.LastName}";
        }
    }
}
