namespace Hooligan.Web;

public class HooliganApiClient(HttpClient httpClient)
{
    public async Task<Association?> CreateAssociation(string first, string second)
    {
        var response = await httpClient.PostAsJsonAsync("/api/associations", new CreateAssociation(first, second));
        return await response.Content.ReadFromJsonAsync<Association>();
    }
}

public record Association(string First, string Second, string Result, string Icon);

public record CreateAssociation(string First, string Second);