// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ProfileConstraints.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Domain.Guests.Constants
{
    using Roomed.Domain.Entities;

    /// <summary>
    /// This class holds all of the <see cref="Profile"/> validation constraints.
    /// </summary>
    public static class ProfileConstraints
    {
        // First name constraints

        /// <summary>
        /// An integer defining the first name maximum length.
        /// </summary>
        public const int FirstNameMaxLength = 50;

        /// <summary>
        /// An integer defining the first name minimum length.
        /// </summary>
        public const int FirstNameMinLength = 3;

        // Last name constraints

        /// <summary>
        /// An integer defining the last name maximum length.
        /// </summary>
        public const int LastNameMaxLength = 50;

        /// <summary>
        /// An integer defining the last name minimum length.
        /// </summary>
        public const int LastNameMinLength = 3;

        // Middle name constraints

        /// <summary>
        /// An integer defining the middle name maximum length.
        /// </summary>
        public const int MiddleNameMaxLength = 50;

        /// <summary>
        /// An integer defining the middle name minimum length.
        /// </summary>
        public const int MiddleNameMinLength = 3;

        // Nationality constraints

        /// <summary>
        /// An integer defining the nationality maximum length.
        /// </summary>
        public const int NationalityMaxLength = 60;

        /// <summary>
        /// An integer defining the nationality minimum length.
        /// </summary>
        public const int NationalityMinLength = 2;

        // Nationality code constraints

        /// <summary>
        /// An integer defining the nationality code maximum length.
        /// </summary>
        public const int NationalityCodeMaxLength = 2;

        /// <summary>
        /// An integer defining the nationality code minimum length.
        /// </summary>
        public const int NationalityCodeMinLength = 2;

        // Address constraints

        /// <summary>
        /// An integer defining the address maximum length.
        /// </summary>
        public const int AddressMaxLength = 250;

        /// <summary>
        /// An integer defining the address minimum length.
        /// </summary>
        public const int AddressMinLength = 10;
    }
}
