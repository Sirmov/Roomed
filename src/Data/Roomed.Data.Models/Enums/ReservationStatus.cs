// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationStatus.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Models.Enums
{
    /// <summary>
    /// This enumeration contains the allowed reservation statuses.
    /// </summary>
    public enum ReservationStatus
    {
        /// <summary>
        /// Indicates that the reservation is expected to arrive in the future.
        /// </summary>
        Expected,

        /// <summary>
        /// Indicates that the reservation is arriving today.
        /// </summary>
        Arriving,

        /// <summary>
        /// Indicates that the reservation is accommodated.
        /// </summary>
        InHouse,

        /// <summary>
        /// Indicates that the reservation is departing today.
        /// </summary>
        Departuring,

        /// <summary>
        /// Indicates that the reservation is checked out.
        /// </summary>
        CheckOut,

        /// <summary>
        /// Indicates that the reservation is canceled.
        /// </summary>
        Canceled,
    }
}
