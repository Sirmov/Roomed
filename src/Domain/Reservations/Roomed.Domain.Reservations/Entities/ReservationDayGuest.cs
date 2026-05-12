// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationDayGuest.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Domain.Reservations.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Domain.Common.Entities;

    /// <summary>
    /// Reservation day guest mapping entity model. Inherits <see cref="BaseDeletableModel{TKey}"/>. Has <see cref="int"/> id.
    /// </summary>
    public class ReservationDayGuest : BaseDeletableModel<int>
    {
        /// <summary>
        /// Gets or sets reservation day id foreign key.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public Guid ReservationDayId { get; set; }

        /// <summary>
        /// Gets or sets guest id foreign key.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public Guid GuestId { get; set; }

        #region  Navigational Properties

        /// <summary>
        /// Gets or sets reservation day navigational property.
        /// </summary>
        [ForeignKey(nameof(ReservationDayId))]
        public virtual ReservationDay ReservationDay { get; set; } = null!;

        #endregion
    }
}
