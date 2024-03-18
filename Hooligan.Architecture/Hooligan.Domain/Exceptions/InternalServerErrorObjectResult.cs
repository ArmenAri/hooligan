using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hooligan.Domain.Exceptions;

/// <inheritdoc />
public sealed class InternalServerErrorObjectResult : ObjectResult
{
    /// <inheritdoc />
    public InternalServerErrorObjectResult(object error)
        : base(error)
    {
        StatusCode = StatusCodes.Status500InternalServerError;
    }
}