using System.Text.Json;
using System.Text.Json.Serialization;

// ReSharper disable ClassNeverInstantiated.Global

// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Hooligan.Domain.Eden;

public sealed class EdenResponse
{
    [JsonPropertyName("openai")] public required OpenAi? OpenAi { get; set; }
    public string? Error { get; set; }
}

public sealed class OpenAi
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [JsonPropertyName("status")]
    public Status Status { get; set; }

    [JsonConverter(typeof(JsonStringGeneratedTextConverter))]
    [JsonPropertyName("generated_text")] public required GeneratedText GeneratedText { get; set; }

    [JsonPropertyName("cost")] public decimal Cost { get; set; }
}

public enum Status
{
    Success,
    Fail
}

public sealed class GeneratedText
{
    public string Result { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}

public class JsonStringGeneratedTextConverter : JsonConverter<GeneratedText>
{
    public override GeneratedText? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var json = reader.GetString();
        return json is null ? null : JsonSerializer.Deserialize<GeneratedText>(json);
    }

    public override void Write(Utf8JsonWriter writer, GeneratedText value, JsonSerializerOptions options)
    {
        var json = JsonSerializer.Serialize(value);
        writer.WriteStringValue(json);
    }
}