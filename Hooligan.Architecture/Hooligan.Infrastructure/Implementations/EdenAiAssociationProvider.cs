using Hooligan.Application.Interfaces;
using Hooligan.Domain;
using Hooligan.Domain.OpenAI;
using Newtonsoft.Json;
using RestSharp;

namespace Hooligan.Infrastructure.Implementations
{
    public class EdenAiAssociationProvider : IExternalAssociationProvider
    {
        private const string EdenAiPublicApiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiNzg1NWFmOWItMzlkNi00OTM2LWFhMzUtYzMwOWI2ZjI0MGU2IiwidHlwZSI6ImFwaV90b2tlbiJ9.B2zQkWi-X-0ChfgYFBqkk5utI3BPYhPnGnlpf5WDDCw";
        private const string EdenAiProvider = "openai";

        public async Task<Association> GetNewAsync(string first, string second, CancellationToken cancellationToken = default)
        {
            var options = new RestClientOptions("https://api.edenai.run/v2/text/chat");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("authorization", $"Bearer {EdenAiPublicApiToken}");
            request.AddJsonBody($"{{\"response_as_dict\":true,\"attributes_as_list\":false,\"show_original_response\":false,\"temperature\":0,\"max_tokens\":1000,\"providers\":\"{EdenAiProvider}\",\"text\":\"What is the result item of \\\"{firstItem}\\\" and \\\"{secondItem}\\\" ?\",\"chatbot_global_action\":\"Context: Request for combination of two items Input: Request for a response with a specific result and an icon associated with a combination of two items Output: {{ Result : [Generated Result], Icon: [Associated Icon]}}\"}}", false);

            var response = await client.PostAsync(request);

            var globalAnswer = JsonConvert.DeserializeObject<OpenAIResponse>(response.Content);
            var generatedText = JsonConvert.DeserializeObject<OpenAIAssociationResult>(globalAnswer.openai.generated_text);

            return generatedText;
        }
    }
}
