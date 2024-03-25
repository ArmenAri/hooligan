using Hooligan.Application.Interfaces;
using Hooligan.Common.Messages;
using MassTransit;

namespace Hooligan.Infrastructure.Implementations;

public sealed class AssociationNotifier(IPublishEndpoint publisher) : IAssociationNotifier
{
    public async Task NotifyNew(NewAssociation association)
    {
        await publisher.Publish(association);
    }
}