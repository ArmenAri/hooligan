using Hooligan.Application.Interfaces;
using Hooligan.Application.Structures;
using Hooligan.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Hooligan.Application.Usages;

public sealed record CreateAssociation(string First, string Second) : IRequest<Association>, INormalizeProperties;

public sealed class CreateAssociationHandler(
    IAssociationRepository associationRepository,
    [FromKeyedServices(ServiceKeys.Eden)] IExternalAssociationProvider externalAssociationProvider)
    : IRequestHandler<CreateAssociation, Association>
{
    public async Task<Association> Handle(CreateAssociation request, CancellationToken cancellationToken)
    {
        var canCraft = await associationRepository.CanBeUsedAsync(request.First.ToLower(), request.Second, cancellationToken);
        if (!canCraft)
        {
            throw new ArgumentException("One of the items has not been discorvered yet");
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
            throw new ArgumentException($"Cannot retrieve association from {externalAssociationProvider.GetType()}");
        }

        await associationRepository.CreateAsync(@new, cancellationToken);

        return @new;
    }
}