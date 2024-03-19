namespace Hooligan.Web.Models;

public partial class HooliganResponse<T>
{
    public T? Value { get; private init; }
    public HooliganException? Error { get; private init; }

    public bool IsFail() => Error is not null && Value is null;
    public bool IsSuccess() => Value is not null && Error is null;

    public static async Task<HooliganResponse<T>> FromFailure(HttpResponseMessage? response)
    {
        ArgumentNullException.ThrowIfNull(response);

        return new HooliganResponse<T>
        {
            Error = await response.Content.ReadFromJsonAsync<HooliganException>()
        };
    }

    public static async Task<HooliganResponse<T>> FromSuccess(HttpResponseMessage? response)
    {
        ArgumentNullException.ThrowIfNull(response);

        return new HooliganResponse<T>
        {
            Value = await response.Content.ReadFromJsonAsync<T>()
        };
    }
}