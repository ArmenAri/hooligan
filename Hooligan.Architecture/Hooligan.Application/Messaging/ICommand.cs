using LanguageExt.Common;
using MediatR;

namespace Hooligan.Application.Messaging;

/// <summary>
/// Represents the command interface.
/// </summary>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>;