// |-----------------------------------------------------------------------------------------------------|
// <copyright file="TimeOnlyJsonConverter.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Json.Converters
{
    using System.Globalization;

    using Newtonsoft.Json;

    /// <summary>
    /// This class contains the logic for reading and writing <see cref="TimeOnly"/>.
    /// </summary>
    public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {
        private const string TimeFormat = "HH:mm:ss.FFFFFFF";

        /// <inheritdoc/>
        public override TimeOnly ReadJson(JsonReader reader, Type objectType, TimeOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return TimeOnly.ParseExact((string)reader.Value!, TimeFormat, CultureInfo.InvariantCulture);
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, TimeOnly value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(TimeFormat, CultureInfo.InvariantCulture));
        }
    }
}
