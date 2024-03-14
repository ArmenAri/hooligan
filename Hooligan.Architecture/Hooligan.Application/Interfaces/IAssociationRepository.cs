using System.Diagnostics.Contracts;
using Hooligan.Domain;

namespace Hooligan.Application.Interfaces;

public interface IAssociationRepository
{
    [Pure]
    public Task<Association?> ExistsAsync(string first, string second, CancellationToken cancellationToken = default);
    public Task<int> CreateAsync(Association association, CancellationToken cancellationToken = default);
}