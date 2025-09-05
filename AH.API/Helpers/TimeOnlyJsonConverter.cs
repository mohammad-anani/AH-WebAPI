using System;
using Newtonsoft.Json;

namespace AH.API.Helpers
{
    public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {
        private const string Format = "HH:mm";

        public override void WriteJson(JsonWriter writer, TimeOnly value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(Format));
        }

        public override TimeOnly ReadJson(JsonReader reader, Type objectType, TimeOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var str = reader.Value?.ToString();

            if (TimeOnly.TryParseExact(str, Format, out var time))
                return time;

            throw new JsonSerializationException($"Invalid time format. Expected format: {Format}");
        }
    }
}