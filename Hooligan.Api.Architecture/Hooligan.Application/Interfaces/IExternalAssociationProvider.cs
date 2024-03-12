using Hooligan.Domain;

namespace Hooligan.Application.Interfaces;

public interface IExternalAssociationProvider
{
    public Task<Association> GetNew(string first, string second, CancellationToken cancellationToken = default);
}