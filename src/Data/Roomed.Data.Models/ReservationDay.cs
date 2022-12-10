// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationDay.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Data.Common.Models;

    /// <summary>
    /// Reservation day entity model. Inherits base deletable model. Has guid id.
    /// </summary>
    public class ReservationDay : BaseDeletableModel<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationDay"/> class.
        /// Sets new guid as id and new reservation day guest hash set for guests.
        /// </summary>
        public ReservationDay()
        {
            this.Id = Guid.NewGuid();
            this.ReservationDayGuests = new HashSet<ReservationDayGuest>();
        }

        /// <summary>
        /// Gets or sets reservation id foreign key.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public Guid ReservationId { get; set; }

        /// <summary>
        /// Gets or sets room id foreign key.
        /// </summary>
        [Required]
        public int RoomId { get; set; }

        /// <summary>
        /// Gets or sets reservation day date.
        /// </summary>
        [Required]
        public DateOnly Date { get; set; }

        // Navigational Properties

        /// <summary>
        /// Gets or sets reservation navigational property.
        /// </summary>
        [ForeignKey(nameof(ReservationId))]
        public virtual Reservation Reservation { get; set; } = null!;

        /// <summary>
        /// Gets or sets room navigational property.
        /// </summary>
        [ForeignKey(nameof(RoomId))]
        public virtual Room Room { get; set; } = null!;

        /// <summary>
        /// Gets or sets guest navigational property.
        /// </summary>
        public virtual ICollection<ReservationDayGuest> ReservationDayGuests { get; set; }
    }
}
