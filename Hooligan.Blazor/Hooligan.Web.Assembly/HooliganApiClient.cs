using System.Net;
using System.Net.Http.Json;
using Hooligan.Web.Assembly.Models;

namespace Hooligan.Web.Assembly;

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