namespace Hooligan.Web;

public class HooliganApiClient(HttpClient httpClient)
{
    public async Task<Association?> GetWeatherAsync()
    {
        return await httpClient.GetFromJsonAsync<Association>("/api/associations") ?? null;
    }
}

public record Association(string First, string Second, string Result, string Icon);