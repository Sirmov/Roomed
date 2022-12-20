// |-----------------------------------------------------------------------------------------------------|
// <copyright file="DateOnlyJsonConverter.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Json.Converters
{
    using System.Globalization;

    using Newtonsoft.Json;

    /// <summary>
    /// This class contains the logic for reading and writing <see cref="DateOnly"/>.
    /// </summary>
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private const string DateFormat = "yyyy-MM-dd";

        /// <inheritdoc/>
        public override DateOnly ReadJson(JsonReader reader, Type objectType, DateOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return DateOnly.ParseExact((string)reader.Value!, DateFormat, CultureInfo.InvariantCulture);
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(DateFormat, CultureInfo.InvariantCulture));
        }
    }
}
