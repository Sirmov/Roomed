// |-----------------------------------------------------------------------------------------------------|
// <copyright file="DateOnlyConverter.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Data.Common
{
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

    /// <summary>
    /// This class inherits <see cref="ValueConverter{TModel, TProvider}"/>
    /// and its used for conversion between <see cref="DateOnly"/> and <see cref="DateTime"/>.
    /// </summary>
    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateOnlyConverter"/> class.
        /// </summary>
        public DateOnlyConverter()
            : base(dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue), dateTime => DateOnly.FromDateTime(dateTime))
        {
        }
    }
}
