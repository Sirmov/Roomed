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

    /// <summary>
    /// This class contains the json settings for reading and writing <see cref="DateOnly"/>.
    /// </summary>
    public class DateOnlyJsonSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateOnlyJsonSettings"/> class.
        /// </summary>
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

        /// <summary>
        /// Gets the <see cref="JsonSerializerSettings"/>.
        /// </summary>
        public JsonSerializerSettings Settings { get; }
    }
}
