using Hooligan.Domain.Exceptions;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace Hooligan.Presentation.Extensions;

/// <summary>
///     <see cref="LanguageExtensions" /> contains a static method that process a result object and return the relevant
///     response.
/// </summary>
public static class LanguageExtensions
{
    /// <summary>
    ///     Matching the given <see cref="Result{A}" /> with generic failure processing.
    /// </summary>
    /// <param name="result">
    ///     The <see cref="Result{A}" /> to match.
    /// </param>
    /// <param name="success">
    ///     The success callback.
    /// </param>
    /// <typeparam name="TResult">
    ///     The type of the <see cref="Result{A}" />.
    /// </typeparam>
    /// <returns>
    ///     <see cref="ActionResult" /> or Exception <see cref="ObjectResult" /> depending on the match.
    /// </returns>
    public static ActionResult Match<TResult>(this Result<TResult> result, Func<TResult, ActionResult> success)
    {
        return result.Match<ActionResult>(
            success,
            failure =>
                failure.ProcessFailure());
    }

    /// <summary>
    ///     Transform an <see cref="Exception" /> to <see cref="ActionResult" />.
    /// </summary>
    /// <param name="exception">
    ///     The exception to process.
    /// </param>
    /// <returns>
    ///     <see cref="IActionResult" /> depending on match.
    /// </returns>
    private static ActionResult ProcessFailure(this Exception exception)
    {
        return exception switch
        {
            BadRequestException badRequest => new BadRequestObjectResult(badRequest),
            InternalServerException internalServerException => new InternalServerErrorObjectResult(
                internalServerException),
            _ => new InternalServerErrorObjectResult("Unexpected error occured.")
        };
    }
}