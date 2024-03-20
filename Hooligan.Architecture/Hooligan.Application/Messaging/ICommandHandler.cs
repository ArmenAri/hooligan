using LanguageExt.Common;
using MediatR;

namespace Hooligan.Application.Messaging;


/// <summary>
/// Represents the command handler interface.
/// </summary>
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;