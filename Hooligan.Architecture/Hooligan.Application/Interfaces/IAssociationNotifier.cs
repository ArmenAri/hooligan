using Hooligan.Domain;
using Hooligan.Messages;

namespace Hooligan.Application.Interfaces;

public interface IAssociationNotifier
{
    public Task NotifyNew(NewAssociation association);
}