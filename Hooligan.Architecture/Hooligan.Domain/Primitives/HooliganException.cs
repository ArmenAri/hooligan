using System.Net;

namespace Hooligan.Domain.Primitives;

/// <summary>
///     <see cref="HooliganException" /> is the class that represents the generic
///     error sent by the server to inform client that something went wrong.
/// </summary>
public class HooliganException : Exception
{
    protected HooliganException(HooliganErrors error, string message, HttpStatusCode statusCode) : base(message)
    {
        Type = GetType().Name;
        Error = error;
        StatusCode = statusCode;
    }

    public string Type { get; }
    public HooliganErrors Error { get; }
    public HttpStatusCode StatusCode { get; }
}