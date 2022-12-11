// |-----------------------------------------------------------------------------------------------------|
// <copyright file="BaseModel.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// This abstract class is the base for every entity.
    /// </summary>
    /// <typeparam name="TKey">The type of the primary key of the entity.</typeparam>
    public abstract class BaseModel<TKey> : IAuditInfo
    {
        /// <summary>
        /// Gets or sets the primary key of the entity.
        /// </summary>
        [Key]
        public TKey Id { get; set; } = default(TKey) !;

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        public DateTime? ModifiedOn { get; set; }
    }
}
