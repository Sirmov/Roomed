// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationNoteConstraints.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Domain.Reservations.Constants
{
    using Roomed.Domain.Reservations.Entities;

    /// <summary>
    /// This class holds all of the <see cref="ReservationNote"/> validation constraints.
    /// </summary>
    public static class ReservationNoteConstraints
    {
        // Reservation note body constraints

        /// <summary>
        /// An integer defining the reservation note body maximum length.
        /// </summary>
        public const int BodyMaxLength = 500;

        /// <summary>
        /// An integer defining the reservation note body minimum length.
        /// </summary>
        public const int BodyMinLength = 5;
    }
}
