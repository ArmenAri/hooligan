using System.Text.Json.Serialization;

namespace Hooligan.Web.Assembly.Models;

public class HooliganException
{
    [JsonPropertyName("type")] public string Type { get; set; }

    [JsonPropertyName("error")] public int Error { get; set; }

    [JsonPropertyName("statusCode")] public int StatusCode { get; set; }

    [JsonPropertyName("targetSite")] public string TargetSite { get; set; }

    [JsonPropertyName("message")] public string Message { get; set; }

    [JsonPropertyName("data")] public object Data { get; set; }

    [JsonPropertyName("innerException")] public string InnerException { get; set; }

    [JsonPropertyName("helpLink")] public string HelpLink { get; set; }

    [JsonPropertyName("source")] public string Source { get; set; }

    [JsonPropertyName("hResult")] public int HResult { get; set; }

    [JsonPropertyName("stackTrace")] public string StackTrace { get; set; }
}