using Hooligan.Application.Interfaces;
using Hooligan.Common.Messages;
using MassTransit;

namespace Hooligan.Infrastructure.Implementations;

public sealed class AssociationNotifier(IPublishEndpoint publisher) : IAssociationNotifier
{
    public async Task NotifyNew(NewAssociation association, CancellationToken cancellationToken)
    {
        await publisher.Publish(association, cancellationToken);
    }
}