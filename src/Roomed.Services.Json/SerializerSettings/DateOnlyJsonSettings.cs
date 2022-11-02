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
                }
            };
        }

        public JsonSerializerSettings Settings { get; }
    }
}
