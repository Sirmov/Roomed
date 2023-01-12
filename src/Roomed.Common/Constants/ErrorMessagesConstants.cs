// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ErrorMessagesConstants.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Common.Constants
{
    /// <summary>
    /// This static class contains all of the error messages.
    /// </summary>
    public class ErrorMessagesConstants
    {
        /// <summary>
        /// A error message indicating that a variable can not be null.
        /// 0 Parameter - The name of the variable.
        /// </summary>
        public const string VariableIsNull = "Variable \"{0}\" cannot be null.";

        /// <summary>
        /// A error message indicating that a argument cannot be null.
        /// 0 Parameter - The name of the argument.
        /// </summary>
        public const string ArgumentIsNull = "Argument \"{0}\" cannot be null.";

        /// <summary>
        /// A error message indicating that a variable can not be null or white space.
        /// 0 Parameter - The name of the variable.
        /// </summary>
        public const string VariableNullOrWhiteSpace = "{0} cannot be null or white space";

        /// <summary>
        /// A error message indicating a variable is set to a null reference.
        /// 0 Parameter - The name of the variable.
        /// </summary>
        public const string NullReference = "{0} is set to a null reference.";

        /// <summary>
        /// A error message indicating that the entity's state is not valid.
        /// 0 Parameter - The name of the entity.
        /// </summary>
        public const string EntitysModelStateIsNotValid = "{0} model state is not valid.";

        /// <summary>
        /// A error message indicating that the entity can not be found.
        /// 0 Parameter - The name of the entity.
        /// </summary>
        public const string EntityNotFound = "The {0} cannot be found.";

        /// <summary>
        /// A error message indicating that the source or destination object is null.
        /// </summary>
        public const string SourceOrDestinationNull = "Source or/and Destination Objects are null";

        /// <summary>
        /// A error message indicating that no entity with specified id exists.
        /// </summary>
        public const string NoEntityWithId = "There is no entity found with this id!";

        /// <summary>
        /// A error message indicating that the script file can not be found.
        /// 0 Parameter - The searched directory.
        /// </summary>
        public const string ScriptNotFound = "Script file can not be found. Searched \"/wwroot/{0}\".";

        /// <summary>
        /// A error message indicating that a controller can not be assumed and should be specified.
        /// </summary>
        public const string ControlledCanNotBeAssuemd = "Controller can not be assumed, it has to be specified.";

        /// <summary>
        /// A error message indicating that the deserialization was not successful.
        /// </summary>
        public const string DeserializationFailed = "Deserialization was not successful.";

        /// <summary>
        /// A error message indicating that a reservation can not be made in the past.
        /// </summary>
        public const string ReservationInThePast = "Cannot make a reservation for the past.";

        /// <summary>
        /// A error message indicating that a reservation already has reservation days.
        /// </summary>
        public const string ExistingReservationDays = "There are reservation days for this reservation already.";

        /// <summary>
        /// A error message indicating that no entity with specified property can be found.
        /// 0 Parameter - The name of the entity.
        /// 1 Parameter - The name of the property.
        /// </summary>
        public const string NoEntityWithPropertyFound = "No {0} with this {1} was found.";
    }
}
