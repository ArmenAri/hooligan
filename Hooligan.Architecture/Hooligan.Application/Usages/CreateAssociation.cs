using Hooligan.Application.Interfaces;
using Hooligan.Domain;
using MediatR;

namespace Hooligan.Application.Usages;

public sealed record CreateAssociation(string First, string Second) : IRequest<Association>;

public sealed class CreateAssociationHandler(IAssociationRepository associationRepository, IExternalAssociationProvider externalAssociationProvider)
    : IRequestHandler<CreateAssociation, Association>
{
    public async Task<Association> Handle(CreateAssociation request, CancellationToken cancellationToken)
    {
        var association = await associationRepository.ExistsAsync(request.First, request.Second, cancellationToken);

        if (association is not null)
        {
            return association;
        }

        // TODO : Create the association calling AI

        var @new = await externalAssociationProvider.GetNewAsync(request.First, request.Second, cancellationToken);
        await associationRepository.CreateAsync(@new, cancellationToken);

        return @new;
    }
}