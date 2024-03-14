using Hooligan.Domain;

namespace Hooligan.Application.Interfaces;

public interface IExternalAssociationProvider
{
    public Task<Association> GetNewAsync(string first, string second, CancellationToken cancellationToken = default);
}