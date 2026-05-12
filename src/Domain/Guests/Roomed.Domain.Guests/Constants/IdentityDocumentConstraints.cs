// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IdentityDocumentConstraints.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Domain.Guests.Constants
{
    using Roomed.Domain.Entities;

    /// <summary>
    /// This class holds all of the <see cref="IdentityDocument"/> validation constraints.
    /// </summary>
    public static class IdentityDocumentConstraints
    {
        // Nationality constraints

        /// <summary>
        /// An integer defining the nationality maximum length.
        /// </summary>
        public const int NationalityMaxLength = 20;

        /// <summary>
        /// An integer defining the nationality minimum length.
        /// </summary>
        public const int NationalityMinLength = 2;

        // Issued by constraints

        /// <summary>
        /// An integer defining the issued by maximum length.
        /// </summary>
        public const int IssuedByMaxLength = 50;

        /// <summary>
        /// An integer defining the issued by minimum length.
        /// </summary>
        public const int IssuedByMinLength = 3;

        // Document number constraints

        /// <summary>
        /// An integer defining the document number maximum length.
        /// </summary>
        public const int NumberMaxLength = 50;

        /// <summary>
        /// An integer defining the document number minimum length.
        /// </summary>
        public const int NumberMinLength = 5;

        // Name constraints

        /// <summary>
        /// An integer defining the name maximum length.
        /// </summary>
        public const int NameMaxLength = 80;

        /// <summary>
        /// An integer defining the name minimum length.
        /// </summary>
        public const int NameMinLength = 3;

        // Personal number constraints

        /// <summary>
        /// An integer defining the personal number maximum length.
        /// </summary>
        public const int PersonalNumberMaxLength = 30;

        /// <summary>
        /// An integer defining the personal number minimum length.
        /// </summary>
        public const int PersonalNumberMinLength = 5;

        // Country constraints

        /// <summary>
        /// An integer defining the country maximum length.
        /// </summary>
        public const int CountryMaxLength = 50;

        /// <summary>
        /// An integer defining the country minimum length.
        /// </summary>
        public const int CountryMinLength = 3;

        // Place of birth constraints

        /// <summary>
        /// An integer defining the place of birth maximum length.
        /// </summary>
        public const int PlaceOfBirthMaxLength = 50;

        /// <summary>
        /// An integer defining the place of birth minimum length.
        /// </summary>
        public const int PlaceOfBirthMinLength = 3;
    }
}
