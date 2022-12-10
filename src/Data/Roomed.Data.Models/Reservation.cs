// |-----------------------------------------------------------------------------------------------------|
// <copyright file="Reservation.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Data.Common.Models;
    using Roomed.Data.Models.Enums;

    using static Roomed.Common.DataConstants.Reservation;

    /// <summary>
    /// Reservation entity model. Inherits base deletable model. Has guid id.
    /// </summary>
    public class Reservation : BaseDeletableModel<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Reservation"/> class.
        /// Sets new guid as id and new hash sets for reservation days and notes.
        /// </summary>
        public Reservation()
        {
            this.Id = Guid.NewGuid();
            this.ReservationDays = new HashSet<ReservationDay>();
            this.Notes = new HashSet<ReservationNote>();
        }

        /// <summary>
        /// Gets or sets reservation holder id foreign key.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public Guid ReservationHolderId { get; set; }

        /// <summary>
        /// Gets or sets reservation arrival date.
        /// </summary>
        [Required]
        public DateOnly ArrivalDate { get; set; }

        /// <summary>
        /// Gets or sets reservation departure date.
        /// </summary>
        [Required]
        public DateOnly DepartureDate { get; set; }

        /// <summary>
        /// Gets or sets reservation status.
        /// </summary>
        [Required]
        [EnumDataType(typeof(ReservationStatus))]
        public ReservationStatus Status { get; set; }

        /// <summary>
        /// Gets or sets room type id foreign key.
        /// </summary>
        [Required]
        public int RoomTypeId { get; set; }

        /// <summary>
        /// Gets or sets reservation adults count.
        /// </summary>
        [Required]
        [Range(0, AdultsMaxCount)]
        public int Adults { get; set; }

        /// <summary>
        /// Gets or sets reservation teenagers count.
        /// </summary>
        [Required]
        [Range(0, TeenagersMaxCount)]
        public int Teenagers { get; set; }

        /// <summary>
        /// Gets or sets reservation children count.
        /// </summary>
        [Required]
        [Range(0, ChildrenMaxCount)]
        public int Children { get; set; }

        // Navigational Properties

        /// <summary>
        /// Gets or sets reservation holder navigational property.
        /// </summary>
        [ForeignKey(nameof(ReservationHolderId))]
        public virtual Profile ReservationHolder { get; set; } = null!;

        /// <summary>
        /// Gets or sets room type navigational property.
        /// </summary>
        [ForeignKey(nameof(RoomTypeId))]
        public virtual RoomType RoomType { get; set; } = null!;

        /// <summary>
        /// Gets or sets reservation days navigational property.
        /// </summary>
        public virtual ICollection<ReservationDay> ReservationDays { get; set; }

        /// <summary>
        /// Gets or sets notes navigational property.
        /// </summary>
        public virtual ICollection<ReservationNote> Notes { get; set; }
    }
}
