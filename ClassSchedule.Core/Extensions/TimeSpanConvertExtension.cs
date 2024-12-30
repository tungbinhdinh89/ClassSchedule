using Newtonsoft.Json;

public static class TimeSpanConvertExtension
{
    public static void UseTimeSpanConverter(this JsonSerializerSettings settings)
    {
        settings.Converters.Add(new UseTimeSpanConverter());
    }
}

public class UseTimeSpanConverter : JsonConverter<TimeSpan>
{
    public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
    {
        writer.WriteValue(DateTime.Today.Add(value).ToString("hh:mm tt"));
    }

    public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var timeString = reader.Value?.ToString();
        if (string.IsNullOrEmpty(timeString))
        {
            throw new JsonSerializationException("Invalid TimeSpan format.");
        }

        if (DateTime.TryParse(timeString, out var parsedTime))
        {
            return parsedTime.TimeOfDay;
        }

        throw new JsonSerializationException($"Invalid TimeSpan format: {timeString}");
    }
}
