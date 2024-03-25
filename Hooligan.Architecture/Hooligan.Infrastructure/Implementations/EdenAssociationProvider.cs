using Hooligan.Application.Interfaces;
using Hooligan.Common.Extensions;
using Hooligan.Domain;
using Hooligan.Infrastructure.Clients;

namespace Hooligan.Infrastructure.Implementations;

public sealed class EdenAssociationProvider(EdenApiClient edenApiClient) : IExternalAssociationProvider
{
    public async Task<Association?> GetNewAsync(string first, string second,
        CancellationToken cancellationToken = default)
    {
        var edenResponse = await edenApiClient.GetAssociationAsync(first, second);
        if (edenResponse is null)
        {
            return null;
        }

        var response = new Association
        {
            First = first,
            Second = second,
            Result = edenResponse.OpenAi.GeneratedText.Result.ToUpperOnlyFirstCharacterInvariant(),
            Icon = edenResponse.OpenAi.GeneratedText.Icon
        };

        return response;
    }
}