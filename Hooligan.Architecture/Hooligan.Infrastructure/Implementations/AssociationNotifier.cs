using Hooligan.Application.Interfaces;
using Hooligan.Domain;
using Hooligan.Messages;
using MassTransit;

namespace Hooligan.Infrastructure.Implementations;

public class AssociationNotifier(IPublishEndpoint publisher) : IAssociationNotifier
{
    public async Task NotifyNew(NewAssociation association)
    {
        await publisher.Publish(association);
    }
}