// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IdentityDocumentType.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Models.Enums
{
    /// <summary>
    /// This enumeration contains the allowed identity document types.
    /// </summary>
    public enum IdentityDocumentType
    {
        /// <summary>
        /// Indicates that the document is an identity card.
        /// </summary>
        Id,

        /// <summary>
        /// Indicates that the document is a passport.
        /// </summary>
        Passport,

        /// <summary>
        /// Indicates that the document is a driving license.
        /// </summary>
        DrivingLicense,
    }
}
