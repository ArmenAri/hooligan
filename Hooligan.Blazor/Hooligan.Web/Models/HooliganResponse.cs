namespace Hooligan.Web.Models;

public class HooliganResponse<T, E>
{
    public T? Value { get; set; }
    public E? Error { get; set; }

    public bool IsFail() => Error is not null && Value is null;
    public bool IsSuccess() => Value is not null && Error is null;
}