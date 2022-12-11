// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IAuditInfo.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Common.Models
{
    using System;

    /// <summary>
    /// This interfaces states all of the needed properties
    ///  for an entity which we want to keep track of audit info.
    /// </summary>
    public interface IAuditInfo
    {
        /// <summary>
        /// Gets or sets the date and time of the creation of the entity.
        /// </summary>
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last modified.
        /// </summary>
        DateTime? ModifiedOn { get; set; }
    }
}
