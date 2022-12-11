// |-----------------------------------------------------------------------------------------------------|
// <copyright file="BaseDeletableModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Common.Models
{
    using System;

    /// <summary>
    /// This abstract class is the base for every deletable entity.
    /// </summary>
    /// <typeparam name="TKey">The type of the primary key of the entity.</typeparam>
    public abstract class BaseDeletableModel<TKey> : BaseModel<TKey>, IDeletableEntity
    {
        /// <inheritdoc/>
        public bool IsDeleted { get; set; }

        /// <inheritdoc/>
        public DateTime? DeletedOn { get; set; }
    }
}
