// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IDeletableEntity.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Common.Models
{
    using System;

    /// <summary>
    /// This interface states the properties needed for any deletable entity.
    /// </summary>
    public interface IDeletableEntity
    {
        /// <summary>
        /// Gets or sets a value indicating whether the entity is deleted or active.
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was deleted.
        /// </summary>
        DateTime? DeletedOn { get; set; }
    }
}
