using System.Net;
using Hooligan.Web.Client.Models;

namespace Hooligan.Web.Client;

public class HooliganApiClient(HttpClient httpClient)
{
    public async Task<HooliganResponse<Association>> CreateAssociation(
        CreateAssociationCommand createAssociationCommand)
    {
        HttpResponseMessage? response;

        try
        {
            response = await httpClient.PostAsJsonAsync("/api/associations", createAssociationCommand);
        }
        catch
        {
            return HooliganResponse<Association>.UnknownException;
        }

        if (response.StatusCode != HttpStatusCode.OK)
        {
            return await HooliganResponse<Association>.FromFailure(response);
        }

        return await HooliganResponse<Association>.FromSuccess(response);
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
public sealed record Association(string? Result, string? Icon);

public record CreateAssociationCommand(string First, string Second);