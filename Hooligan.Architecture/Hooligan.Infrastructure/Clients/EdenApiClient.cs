using System.Text.Json;
using Hooligan.Domain.Eden;
using Hooligan.Domain.Structures;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Hooligan.Infrastructure.Clients;

public sealed class EdenApiClient
{
    private readonly RestClient _restClient;
    private readonly string? _token;

    public EdenApiClient(IConfiguration configuration)
    {
        _token = configuration["EdenAI:Token"];
        ArgumentNullException.ThrowIfNull(_token);

        var restClientOptions = new RestClientOptions("https://api.edenai.run/v2/text/chat")
        {
            ThrowOnAnyError = true
        };
        _restClient = new RestClient(restClientOptions);
    }

    public async Task<EdenResponse> GetAssociationAsync(string first, string second)
    {
        var request = new RestRequest("");
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {_token}");

        var edenRequest = new EdenRequest
        {
            MaxTokens = 1000,
            Providers = Providers.OpenAi,
            Text = $"What is the result of {first} and {second} ?",
            ChatbotGlobalAction =
                "Context: Request for combination of two items based on merge games and infinite crafting including pop-culture references when it is necessary. I don't want you to concatenate inputs for the result. Input: Request for a response with a specific existing and plausible one-word result and only one emoji that represents the result of the two inputs Output: {\"Result\" : \"[Generated Result]\", \"Icon\": \"[Associated Emoji]\"}"
        };

        request.AddJsonBody(JsonSerializer.Serialize(edenRequest, JsonSerializerOptions.Web), false);

        try
        {
            var restResponse = await _restClient.PostAsync(request);
            ArgumentNullException.ThrowIfNull(restResponse.Content);
            var response = JsonSerializer.Deserialize<EdenResponse>(restResponse.Content);

            if (response is null)
            {
                return new EdenResponse
                {
                    OpenAi = null,
                    Error = "Serialization error occured"
                };
            }

            return response;
        }
        catch (Exception e)
        {
            return new EdenResponse
            {
                OpenAi = null,
                Error = e.Message
            };
        }
    }
}