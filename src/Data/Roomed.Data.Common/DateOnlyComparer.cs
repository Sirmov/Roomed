// |-----------------------------------------------------------------------------------------------------|
// <copyright file="DateOnlyComparer.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Common
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    /// <summary>
    /// This class inherits <see cref="ValueComparer{T}"/>
    /// and its used for comparing <see cref="DateOnly"/> and <see cref="DateTime"/>.
    /// </summary>
    public class DateOnlyComparer : ValueComparer<DateOnly>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateOnlyComparer"/> class.
        /// </summary>
        public DateOnlyComparer()
            : base((d1, d2) => d1.DayNumber == d2.DayNumber, d => d.GetHashCode())
        {
        }
    }
}
