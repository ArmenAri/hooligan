using System.Net;
using Hooligan.Domain.Primitives;

namespace Hooligan.Domain.Exceptions;

/// <summary>
///     <see cref="InternalServerException" /> is thrown when an unknown error occurs.
/// </summary>
public sealed class InternalServerException(HooliganErrors error, string message)
    : HooliganException(error, message, HttpStatusCode.InternalServerError);