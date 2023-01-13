// |-----------------------------------------------------------------------------------------------------|
// <copyright file="DataKeyConstants.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Common.Constants
{
    /// <summary>
    /// This static class contains all of the string based keys used
    /// in ViewData, TempData and other dictionaries using string keys.
    /// </summary>
    public static class DataKeyConstants
    {
        // ViewData

        /// <summary>
        /// A reservations type dictionary string key.
        /// </summary>
        public const string ReservationsType = "ReservationsType";

        /// <summary>
        /// A reservation input model dictionary string key.
        /// </summary>
        public const string ReservationInputModel = "ReservationInputModel";

        /// <summary>
        /// A free rooms dictionary string key.
        /// </summary>
        public const string FreeRooms = "FreeRooms";

        /// <summary>
        /// A all roles dictionary string key.
        /// </summary>
        public const string AllRoles = "AllRoles";

        // TempData

        /// <summary>
        /// A error title dictionary string key.
        /// </summary>
        public const string ErrorTitle = "ErrorTitle";

        /// <summary>
        /// A error message dictionary string key.
        /// </summary>
        public const string ErrorMessage = "ErrorMessage";

        /// <summary>
        /// A create reservation model dictionary string key.
        /// </summary>
        public const string CreateReservationModel = "CreateReservationModel";
    }
}
