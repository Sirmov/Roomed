﻿namespace Roomed.Web.ViewModels.Profile
{
    using AutoMapper.Configuration.Annotations;
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Dtos.Profile;
    using Roomed.Services.Mapping;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.Profile"/> view model.
    /// </summary>
    public class DetailedProfileViewModel : IMapFrom<DetailedProfileDto>
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = null!;

        public DateOnly? Birthdate { get; set; }

        public Gender? Gender { get; set; }

        public string? Nationality { get; set; }

        public string? NationalityCode { get; set; }

        public string? Address { get; set; }

        [Ignore]
        public string FullName
        {
            get => $"{this.FirstName} {(this.MiddleName == null ? string.Empty : $"{this.MiddleName} ")}{this.LastName}";
        }
    }
}