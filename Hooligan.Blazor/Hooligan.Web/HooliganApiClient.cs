using System.Net;
using Hooligan.Web.Models;

namespace Hooligan.Web;

public class HooliganApiClient(HttpClient httpClient)
{
    public async Task<HooliganResponse<Association, HooliganException>> CreateAssociation(
        CreateAssociationCommand createAssociationCommand)
    {
        var response = await httpClient.PostAsJsonAsync("/api/associations", createAssociationCommand);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            return new HooliganResponse<Association, HooliganException>
            {
                Error = await response.Content.ReadFromJsonAsync<HooliganException>()
            };
        }

        return new HooliganResponse<Association, HooliganException>
        {
            Value = await response.Content.ReadFromJsonAsync<Association>()
        };
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
public sealed record Association(string? Result, string? Icon);

public record CreateAssociationCommand(string First, string Second);