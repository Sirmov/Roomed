// |-----------------------------------------------------------------------------------------------------|
// <copyright file="RoomConstraints.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Domain.Facility.Constants
{
    using Roomed.Domain.Facility.Entities;

    /// <summary>
    /// This class holds all of the <see cref="Room"/> validation constraints.
    /// </summary>
    public static class RoomConstraints
    {
        // Room number constraints

        /// <summary>
        /// An integer defining the room number maximum length.
        /// </summary>
        public const int RoomNumberMaxLength = 6;
    }
}
