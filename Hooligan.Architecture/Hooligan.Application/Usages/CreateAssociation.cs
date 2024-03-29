using Hooligan.Application.Interfaces;
using Hooligan.Application.Messaging;
using Hooligan.Application.Structures;
using Hooligan.Domain;
using Hooligan.Domain.Exceptions;
using Hooligan.Domain.Primitives;
using Hooligan.Common;
using Hooligan.Common.Messages;
using LanguageExt.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Hooligan.Application.Usages;

public sealed record CreateAssociation(string First, string Second) : ICommand<Association>, INormalizeProperties;

public sealed class CreateAssociationHandler(
    IAssociationRepository associationRepository,
    [FromKeyedServices(ServiceKeys.Eden)] IExternalAssociationProvider externalAssociationProvider,
    IAssociationNotifier associationNotifier)
    : ICommandHandler<CreateAssociation, Association>
{
    public async Task<Result<Association>> Handle(CreateAssociation request, CancellationToken cancellationToken)
    {
        var canCraft = await associationRepository.CanBeUsedAsync(request.First, request.Second, cancellationToken);
        if (!canCraft)
        {
            return new Result<Association>(new BadRequestException(HooliganErrors.NotYetDiscovered,
                "One of the items has not been discovered yet"));
        }

        var association = await associationRepository.ExistsAsync(request.First, request.Second, cancellationToken);

        if (association is not null)
        {
            return association;
        }

        var @new = await externalAssociationProvider.GetNewAsync(
            request.First,
            request.Second,
            cancellationToken);

        return await @new.Match(Succeed, Failed);

        async Task<Result<Association>> Succeed(Association success)
        {
            await associationRepository.CreateAsync(success, cancellationToken);
            await associationNotifier.NotifyNew(new NewAssociation(success.Result));
            return success;
        }

        Task<Result<Association>> Failed(Exception exception)
        {
            return Task.FromResult(new Result<Association>(exception));
        }
    }
}