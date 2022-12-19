// |-----------------------------------------------------------------------------------------------------|
// <copyright file="DateOnlyJsonSettings.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Json.SerializerSettings
{
    using Newtonsoft.Json;
    using Roomed.Services.Json.Converters;

    public class DateOnlyJsonSettings
    {
        public DateOnlyJsonSettings()
        {
            this.Settings = new JsonSerializerSettings()
            {
                Converters = new List<JsonConverter>()
                {
                    new NullableDateOnlyJsonConverter(),
                    new DateOnlyJsonConverter(),
                },
            };
        }

        public JsonSerializerSettings Settings { get; }
    }
}
