// |-----------------------------------------------------------------------------------------------------|
// <copyright file="Profile.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Roomed.Data.Common.Models;
    using Roomed.Data.Models.Enums;

    using static Roomed.Common.DataConstants.Profile;

    /// <summary>
    /// Profile entity model. Inherits base deletable model. Has guid id.
    /// </summary>
    public class Profile : BaseDeletableModel<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class with new guid as id.
        /// Sets holder reservations, guest reservation days, identity documents and notes as new hash sets.
        /// </summary>
        public Profile()
        {
            this.Id = Guid.NewGuid();
            this.HolderReservations = new HashSet<Reservation>();
            this.GuestReservationDays = new HashSet<ReservationDayGuest>();
            this.IdentityDocuments = new HashSet<IdentityDocument>();
            this.Notes = new HashSet<ProfileNote>();
        }

        /// <summary>
        /// Gets or sets profile first name.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Gets or sets profile middle name.
        /// </summary>
        [MaxLength(MiddleNameMaxLenght)]
        public string? MiddleName { get; set; }

        /// <summary>
        /// Gets or sets profile last name.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Gets or sets profile birthday.
        /// </summary>
        public DateOnly? Birthdate { get; set; }

        /// <summary>
        /// Gets or sets profile gender.
        /// </summary>
        [EnumDataType(typeof(Gender))]
        public Gender? Gender { get; set; }

        /// <summary>
        /// Gets or sets profile nationality.
        /// </summary>
        [MaxLength(NationalityMaxLength)]
        public string? Nationality { get; set; }

        /// <summary>
        /// Gets or sets profile nationality code.
        /// </summary>
        [StringLength(NationalityCodeMaxLength, MinimumLength = NationalityCodeMinLength)]
        public string? NationalityCode { get; set; }

        /// <summary>
        /// Gets or sets profile address.
        /// </summary>
        [MaxLength(AddressMaxLength)]
        public string? Address { get; set; }

        // Navigational Properties

        /// <summary>
        /// Gets or sets holder reservations navigational property.
        /// </summary>
        public virtual ICollection<Reservation> HolderReservations { get; set; }

        /// <summary>
        /// Gets or sets guest reservation days navigational property.
        /// </summary>
        public virtual ICollection<ReservationDayGuest> GuestReservationDays { get; set; }

        /// <summary>
        /// Gets or sets identity documents navigational property.
        /// </summary>
        public virtual ICollection<IdentityDocument> IdentityDocuments { get; set; }

        /// <summary>
        /// Gets or sets profile notes property.
        /// </summary>
        public virtual ICollection<ProfileNote> Notes { get; set; }
    }
}
