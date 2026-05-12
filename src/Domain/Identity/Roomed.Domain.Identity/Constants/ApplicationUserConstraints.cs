// |-----------------------------------------------------------------------------------------------------|
// <copyright file="ApplicationUserConstraints.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Domain.Identity.Constants
{
    using Roomed.Domain.Identity.Entities;

    /// <summary>
    /// This class holds all of the <see cref="ApplicationUser"/> validation constraints.
    /// </summary>
    public static class ApplicationUserConstraints
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
}
