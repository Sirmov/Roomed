// |-----------------------------------------------------------------------------------------------------|
// <copyright file="RoomTypeConstraints.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Domain.Facility.Constants
{
    using Roomed.Domain.Facility.Entities;

    /// <summary>
    /// This class holds all of the <see cref="RoomType"/> validation constraints.
    /// </summary>
    public static class RoomTypeConstraints
    {
        // Room type name constraints

        /// <summary>
        /// An integer defining the room type name maximum length.
        /// </summary>
        public const int NameMaxLength = 50;

        /// <summary>
        /// An integer defining the room type name minimum length.
        /// </summary>
        public const int NameMinLength = 5;
    }
}
