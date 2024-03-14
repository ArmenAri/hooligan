using Hooligan.Application.Interfaces;
using Hooligan.Domain;
using Hooligan.Domain.OriginalsItems;
using Hooligan.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Hooligan.Infrastructure.Implementations;

public class AssociationRepository(HooliganDbContext context, OriginalItems originalItems) : IAssociationRepository
{
    public Task<Association?> ExistsAsync(string first, string second, CancellationToken cancellationToken)
    {
        return context.Associations.Where(a => a.First == first && a.Second == second)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<int> CreateAsync(Association association, CancellationToken cancellationToken)
    {
        await context.AddAsync(association, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> CanBeUsedAsync(string first, string second, CancellationToken cancellationToken = default)
    {
        var results = await context.Associations.Select(x => x.Result).ToListAsync();
        var completeResults = results.Concat(originalItems);

        return completeResults.Contains(first) && completeResults.Contains(second);
    }
}