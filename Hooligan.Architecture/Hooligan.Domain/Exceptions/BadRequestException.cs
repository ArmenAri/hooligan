using System.Net;
using Hooligan.Domain.Primitives;

namespace Hooligan.Domain.Exceptions;

/// <summary>
///     <see cref="BadRequestException" /> is thrown when the request is invalid.
/// </summary>
public sealed class BadRequestException(HooliganErrors error, string message)
    : HooliganException(error, message, HttpStatusCode.BadRequest);