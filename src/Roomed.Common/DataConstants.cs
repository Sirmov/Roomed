// |-----------------------------------------------------------------------------------------------------|
// <copyright file="DataConstants.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Common
{
    /// <summary>
    /// This static class contains all of the data models validations constraints.
    /// </summary>
    public static class DataConstants
    {
        /// <summary>
        /// This class holds all of the <see cref="ApplicationUser"/> validation constraints.
        /// </summary>
        public static class ApplicationUser
        {
            // Username constraints

            /// <summary>
            /// An integer defining the username minimum length.
            /// </summary>
            public const int UserNameMinLength = 5;

            /// <summary>
            /// An integer defining the username maximum length.
            /// </summary>
            public const int UserNameMaxLength = 30;

            // Password constraints

            /// <summary>
            /// An integer defining the password minimum length.
            /// </summary>
            public const int PasswordMinLength = 8;

            /// <summary>
            /// An integer defining the password maximum length.
            /// </summary>
            public const int PasswordMaxLength = 30;

            // Email constraints

            /// <summary>
            /// An integer defining the email minimum length.
            /// </summary>
            public const int EmailMinLength = 6;

            /// <summary>
            /// An integer defining the email maximum length.
            /// </summary>
            public const int EmailMaxLength = 35;
        }

        /// <summary>
        /// This class holds all of the <see cref="Reservation"/> validation constraints.
        /// </summary>
        public static class Reservation
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

        /// <summary>
        /// This class holds all of the <see cref="Profile"/> validation constraints.
        /// </summary>
        public static class Profile
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
            public const int MiddleNameMaxLenght = 50;

            /// <summary>
            /// An integer defining the middle name minimum length.
            /// </summary>
            public const int MiddleNameMinLenght = 3;

            // Nationality constraints

            /// <summary>
            /// An integer defining the nationality maximum length.
            /// </summary>
            public const int NationalityMaxLength = 40;

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

        /// <summary>
        /// This class holds all of the <see cref="IdentityDocument"/> validation constraints.
        /// </summary>
        public static class IdentityDocument
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

        /// <summary>
        /// This class holds all of the <see cref="Room"/> validation constraints.
        /// </summary>
        public static class Room
        {
            // Room number constraints

            /// <summary>
            /// An integer defining the room number maximum length.
            /// </summary>
            public const int RoomNumberMaxLength = 6;
        }

        /// <summary>
        /// This class holds all of the <see cref="RoomType"/> validation constraints.
        /// </summary>
        public static class RoomType
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

        /// <summary>
        /// This class holds all of the <see cref="ReservationNote"/> validation constraints.
        /// </summary>
        public static class ReservationNote
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

        /// <summary>
        /// This class holds all of the <see cref="ProfileNote"/> validation constraints.
        /// </summary>
        public static class ProfileNote
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
}
