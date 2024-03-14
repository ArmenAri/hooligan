using System.Text.Json.Serialization;

namespace Hooligan.Domain.Eden;

public class EdenRequest
{
    [JsonPropertyName("response_as_dict")] public bool ResponseAsDict { get; set; } = true;

    [JsonPropertyName("attributes_as_list")]
    public bool AttributesAsList { get; set; }

    [JsonPropertyName("show_original_response")]
    public bool ShowOriginalResponse { get; set; }

    [JsonPropertyName("temperature")] public int Temperature { get; set; }

    [JsonPropertyName("max_tokens")] public int MaxTokens { get; set; }

    [JsonPropertyName("providers")] public required string Providers { get; set; }

    [JsonPropertyName("text")] public required string Text { get; set; }

    [JsonPropertyName("chatbot_global_action")]
    public required string ChatbotGlobalAction { get; set; }
}