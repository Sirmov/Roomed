// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ProfileNoteConstraints.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Domain.Guests.Constants
{
    using Roomed.Domain.Entities;

    /// <summary>
    /// This class holds all of the <see cref="ProfileNote"/> validation constraints.
    /// </summary>
    public static class ProfileNoteConstraints
    {
        // Profile note body constraints

        /// <summary>
        /// An integer defining the profile note body maximum length.
        /// </summary>
        public const int BodyMaxLength = 500;

        /// <summary>
        /// An integer defining the profile note body minimum length.
        /// </summary>
        public const int BodyMinLength = 5;
    }
}
