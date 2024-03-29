﻿// |-----------------------------------------------------------------------------------------------------|
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
        /// A error message indicating that a variable cannot be null.
        /// 0 Parameter - The name of the variable.
        /// </summary>
        public const string VariableIsNull = "Variable \"{0}\" cannot be null.";

        /// <summary>
        /// A error message indicating that a variable cannot be true.
        /// </summary>
        public const string VariableIsTrue = "Variable \"{0}\" cannot be true.";

        /// <summary>
        /// A error message indicating that a variable cannot be false.
        /// </summary>
        public const string VariableIsFalse = "Variable \"{0}\" cannot be false.";

        /// <summary>
        /// A error message indicating that a argument cannot be null.
        /// 0 Parameter - The name of the argument.
        /// </summary>
        public const string ArgumentIsNull = "Argument \"{0}\" cannot be null.";

        /// <summary>
        /// A error message indicating that a argument cannot be true.
        /// </summary>
        public const string ArgumentIsTrue = "Argument \"{0}\" cannot be true.";

        /// <summary>
        /// A error message indicating that a argument cannot be false.
        /// </summary>
        public const string ArgumentIsFalse = "Argument \"{0}\" cannot be false.";

        /// <summary>
        /// A error message indicating that a variable cannot be null or white space.
        /// 0 Parameter - The name of the variable.
        /// </summary>
        public const string VariableIsNullOrWhiteSpace = "Variable \"{0}\" cannot be null or white space.";

        /// <summary>
        /// A error message indicating that a argument cannot be null or white space.
        /// 0 Parameter - The name of the variable.
        /// </summary>
        public const string ArgumentIsNullOrWhiteSpace = "Argument \"{0}\" cannot be null or white space.";

        /// <summary>
        /// A error message indicating that a variable cannot be null or empty.
        /// 0 Parameter - The name of the variable.
        /// </summary>
        public const string VariableIsNullOrEmpty = "Variable \"{0}\" cannot be null or empty.";

        /// <summary>
        /// A error message indicating that a argument cannot be null or empty.
        /// 0 Parameter - The name of the variable.
        /// </summary>
        public const string ArgumentIsNullOrEmpty = "Argument \"{0}\" cannot be null or empty.";

        /// <summary>
        /// A error message indicating that a string variable does not match a regex pattern.
        /// 0 Parameter - The name of the variable.
        /// 1 Parameter - The string value of the variable.
        /// 2 Parameter - The regex pattern.
        /// </summary>
        public const string VariableRegexDoesNotMatch = "Variable {0} does not match the regex pattern.\nString - {1}\nRegex - {2}";

        /// <summary>
        /// A error message indicating that a string argument does not match a regex pattern.
        /// 0 Parameter - The name of the argument.
        /// 1 Parameter - The string value of the argument.
        /// 2 Parameter - The regex pattern.
        /// </summary>
        public const string ArgumentRegexDoesNotMatch = "Variable {0} does not match the regex pattern.\nString - {1}\nRegex - {2}";

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
        /// A error message indicating that the entity cannot be found.
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
        /// A error message indicating that the script file cannot be found.
        /// 0 Parameter - The searched directory.
        /// </summary>
        public const string ScriptNotFound = "Script file cannot be found. Searched \"/wwroot/{0}\".";

        /// <summary>
        /// A error message indicating that a controller cannot be assumed and should be specified.
        /// </summary>
        public const string ControlledCanNotBeAssuemd = "Controller cannot be assumed, it has to be specified.";

        /// <summary>
        /// A error message indicating that the deserialization was not successful.
        /// </summary>
        public const string DeserializationFailed = "Deserialization was not successful.";

        /// <summary>
        /// A error message indicating that a reservation cannot be made in the past.
        /// </summary>
        public const string ReservationInThePast = "Cannot make a reservation from the past.";

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
