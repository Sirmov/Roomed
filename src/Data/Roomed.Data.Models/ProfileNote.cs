﻿// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ProfileNote.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Data.Common.Models;

    using static Roomed.Common.Constants.DataConstants.ProfileNote;

    /// <summary>
    /// Profile note entity model. Inherits <see cref="BaseDeletableModel{TKey}"/>. Has <see cref="Guid"/> id.
    /// </summary>
    public class ProfileNote : BaseDeletableModel<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileNote"/> class with new guid as id.
        /// </summary>
        public ProfileNote()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets profile id foreign key.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public Guid ProfileId { get; set; }

        /// <summary>
        /// Gets or sets profile note body.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(BodyMaxLength)]
        public string Body { get; set; } = null!;

        // Navigational Properties

        /// <summary>
        /// Gets or sets profile navigational property.
        /// </summary>
        [ForeignKey(nameof(ProfileId))]
        public virtual Profile Profile { get; set; } = null!;
    }
}
