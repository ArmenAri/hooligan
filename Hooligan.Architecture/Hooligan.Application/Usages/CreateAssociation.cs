using Hooligan.Application.Interfaces;
using Hooligan.Application.Messaging;
using Hooligan.Application.Structures;
using Hooligan.Domain;
using Hooligan.Domain.Exceptions;
using Hooligan.Domain.Primitives;
using LanguageExt.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Hooligan.Application.Usages;

public sealed record CreateAssociation(string First, string Second) : ICommand<Association>, INormalizeProperties;

public sealed class CreateAssociationHandler(
    IAssociationRepository associationRepository,
    [FromKeyedServices(ServiceKeys.Eden)] IExternalAssociationProvider externalAssociationProvider)
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

        if (@new is null)
        {
            return new Result<Association>(new InternalServerException(HooliganErrors.ExternalProviderUnavailable,
                $"An error occured when using {externalAssociationProvider.GetType()}"));
        }

        await associationRepository.CreateAsync(@new, cancellationToken);

        return @new;
    }
}