// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ReservationConstraints.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Domain.Reservations.Constants
{
    using Roomed.Domain.Reservations.Entities;

    /// <summary>
    /// This class holds all of the <see cref="Reservation"/> validation constraints.
    /// </summary>
    public static class ReservationConstraints
    {
        /// <summary>
        /// An integer defining the maximum count of adults.
        /// </summary>
        public const int AdultsMaxCount = 5;

        /// <summary>
        /// An integer defining the maximum count of teenagers.
        /// </summary>
        public const int TeenagersMaxCount = 5;

        /// <summary>
        /// An integer defining the maximum count of children.
        /// </summary>
        public const int ChildrenMaxCount = 5;
    }
}
