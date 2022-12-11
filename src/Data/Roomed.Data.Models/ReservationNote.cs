// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationNote.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Data.Common.Models;

    using static Roomed.Common.DataConstants.ReservationNote;

    /// <summary>
    /// Reservation note entity model. Inherits <see cref="BaseDeletableModel{TKey}"/>. Has <see cref="Guid"/> id.
    /// </summary>
    public class ReservationNote : BaseDeletableModel<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationNote"/> class with new guid as id.
        /// </summary>
        public ReservationNote()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets reservation id foreign key.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public Guid ReservationId { get; set; }

        /// <summary>
        /// Gets or sets reservation note body.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(BodyMaxLength)]
        public string Body { get; set; } = null!;

        // Navigational Properties

        /// <summary>
        /// Gets or sets reservation navigational property.
        /// </summary>
        [ForeignKey(nameof(ReservationId))]
        public virtual Reservation Reservation { get; set; } = null!;
    }
}
