using Hooligan.Application.Interfaces;
using Hooligan.Common.Extensions;
using Hooligan.Domain;
using Hooligan.Domain.Exceptions;
using Hooligan.Domain.Primitives;
using Hooligan.Infrastructure.Clients;
using LanguageExt.Common;

namespace Hooligan.Infrastructure.Implementations;

public sealed class EdenAssociationProvider(EdenApiClient edenApiClient) : IExternalAssociationProvider
{
    public async Task<Result<Association>> GetNewAsync(string first, string second,
        CancellationToken cancellationToken = default)
    {
        var edenResponse = await edenApiClient.GetAssociationAsync(first, second);

        if (edenResponse.OpenAi is not null)
        {
            return Association.Create(first, second,
                edenResponse.OpenAi.GeneratedText.Result.ToUpperOnlyFirstCharacterInvariant(),
                edenResponse.OpenAi.GeneratedText.Icon);
        }

        if (edenResponse.Error is not null)
        {
            return new Result<Association>(new InternalServerException(HooliganErrors.ExternalProviderUnavailable,
                edenResponse.Error));
        }

        return new Result<Association>(new InternalServerException(HooliganErrors.ExternalProviderUnavailable,
            "Unknown error occured"));
    }
}