﻿// |-----------------------------------------------------------------------------------------------------|
// <copyright file="NullableDateOnlyJsonConverter.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Json.Converters
{
    using System.Globalization;

    using Newtonsoft.Json;

    public class NullableDateOnlyJsonConverter : JsonConverter<DateOnly?>
    {
        private const string DateFormat = "yyyy-MM-dd";

        public override DateOnly? ReadJson(JsonReader reader, Type objectType, DateOnly? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string value = (string)reader.Value;

            if (value != null)
            {
                return DateOnly.ParseExact(value, DateFormat, CultureInfo.InvariantCulture);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, DateOnly? value, JsonSerializer serializer)
        {
            if (value.HasValue)
            {
                writer.WriteValue(value.Value.ToString(DateFormat, CultureInfo.InvariantCulture));
            }

            writer.WriteValue(value.ToString());
        }
    }
}
