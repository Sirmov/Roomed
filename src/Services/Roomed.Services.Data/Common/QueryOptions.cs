﻿// |-----------------------------------------------------------------------------------------------------|
// <copyright file="QueryOptions.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Data.Common
{
    /// <summary>
    /// This class is used to modify service queries. That way we eliminate the creating of multiple similar methods.
    /// Example:
    /// <code>GetAll();
    /// GetAllOrderedByName();
    /// GetAllOrderedByNameAsNoTracking();
    /// GetAllOrderedByNameWithDeletedAsNoTracking();
    /// </code>
    /// All of this can be achieved by using like this:
    /// <code>GetAll(QuerryOptions options);</code>
    /// </summary>
    /// <typeparam name="TDto">The query entity.</typeparam>
    public class QueryOptions<TDto>
    {
        /// <summary>
        /// Gets or sets a value indicating whether the entities should be tracked.
        /// </summary>
        public bool IsReadOnly { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether only not deleted entities should be returned.
        /// </summary>
        public bool WithDeleted { get; set; } = false;

        /// <summary>
        /// Gets or sets a list of <see cref="OrderOption{TClass}"/>.
        /// The direct use of <see cref="List{T}"/> instead of <see cref="ICollection{T}"/> or <see cref="IEnumerable{T}"/> is because
        /// the ease of use and readability of the <c>new()</c> operator.
        /// </summary>
        public List<OrderOption<TDto>> OrderOptions { get; set; } = new List<OrderOption<TDto>>();

        /// <summary>
        /// Gets or sets the amount of entities to be skipped.
        /// </summary>
        public int? Skip { get; set; }

        /// <summary>
        /// Gets or sets the amount of entities to be taken.
        /// </summary>
        public int? Take { get; set; }
    }
}
