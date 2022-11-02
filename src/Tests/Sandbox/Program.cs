using Newtonsoft.Json;
using Roomed.Data.Models;
using System.Globalization;

internal class Program
{
    private static  void Main(string[] args)
    {
        string jsonPath = "./Seeding/IdentityDocumentSeed.json";

        string text = File.ReadAllText(jsonPath);

        IEnumerable<IdentityDocument> data = JsonConvert.DeserializeObject<IEnumerable<IdentityDocument>>(text, new DateOnlyJsonConverter());
    }

    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private const string DateFormat = "yyyy-MM-dd";

        public override DateOnly ReadJson(JsonReader reader, Type objectType, DateOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return DateOnly.ParseExact((string)reader.Value, DateFormat, CultureInfo.InvariantCulture);
        }

        public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(DateFormat, CultureInfo.InvariantCulture));
        }
    }

}