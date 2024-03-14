using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hooligan.Domain.OpenAI
{
    public record OpenAIInfo
    {
        public string status { get; set; }
        [JsonConverter(typeof(InnerObjectConverter))]
        public OpenAIAssociationResult generated_text { get; set; }
        public List<OpenAIMessage> message { get; set; }
        public double cost { get; set; }
    }

    internal class InnerObjectConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(OpenAIAssociationResult);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            if (token.Type == JTokenType.String)
            {
                // Deserialize the inner JSON string
                return JsonConvert.DeserializeObject<OpenAIAssociationResult>(token.Value<string>());
            }
            throw new JsonSerializationException("Expected string");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
