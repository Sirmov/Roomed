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
            public const int UserNameMinLength = 5;
            public const int UserNameMaxLength = 30;

            public const int PasswordMinLength = 8;
            public const int PasswordMaxLength = 30;

            public const int EmailMinLength = 6;
            public const int EmailMaxLength = 35;
        }

        /// <summary>
        /// This class holds all of the <see cref="Reservation"/> validation constraints.
        /// </summary>
        public static class Reservation
        {
            public const int AdultsMaxCount = 5;
            public const int TeenagersMaxCount = 5;
            public const int ChildrenMaxCount = 5;
        }

        /// <summary>
        /// This class holds all of the <see cref="Profile"/> validation constraints.
        /// </summary>
        public static class Profile
        {
            public const int FirstNameMaxLength = 50;
            public const int FirstNameMinLength = 3;

            public const int LastNameMaxLength = 50;
            public const int LastNameMinLength = 3;

            public const int MiddleNameMaxLenght = 50;
            public const int MiddleNameMinLenght = 3;

            public const int NationalityMaxLength = 40;
            public const int NationalityMinLength = 2;

            public const int NationalityCodeMaxLength = 2;
            public const int NationalityCodeMinLength = 2;

            public const int AddressMaxLength = 250;
            public const int AddressMinLength = 10;
        }

        /// <summary>
        /// This class holds all of the <see cref="IdentityDocument"/> validation constraints.
        /// </summary>
        public static class IdentityDocument
        {
            public const int NationalityMaxLength = 20;
            public const int NationalityMinLength = 2;

            public const int IssuedByMaxLength = 50;
            public const int IssuedByMinLength = 3;

            public const int NumberMaxLength = 50;
            public const int NumberMinLength = 5;

            public const int NameMaxLength = 80;
            public const int NameMinLength = 3;

            public const int PersonalNumberMaxLength = 30;
            public const int PersonalNumberMinLength = 5;

            public const int CountryMaxLength = 50;
            public const int CountryMinLength = 3;

            public const int PlaceOfBirthMaxLength = 50;
            public const int PlaceOfBirthMinLength = 3;
        }

        /// <summary>
        /// This class holds all of the <see cref="Room"/> validation constraints.
        /// </summary>
        public static class Room
        {
            public const int RoomNumberMaxLength = 6;
        }

        /// <summary>
        /// This class holds all of the <see cref="RoomType"/> validation constraints.
        /// </summary>
        public static class RoomType
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 50;
        }

        /// <summary>
        /// This class holds all of the <see cref="ReservationNote"/> validation constraints.
        /// </summary>
        public static class ReservationNote
        {
            public const int BodyMinLength = 5;
            public const int BodyMaxLength = 500;
        }

        /// <summary>
        /// This class holds all of the <see cref="ProfileNote"/> validation constraints.
        /// </summary>
        public static class ProfileNote
        {
            public const int BodyMinLength = 5;
            public const int BodyMaxLength = 500;
        }
    }
}
