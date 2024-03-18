using Hooligan.Common;
using Hooligan.Common.Messages;

namespace Hooligan.Application.Interfaces;

public interface IAssociationNotifier
{
    public Task NotifyNew(NewAssociation association);
}