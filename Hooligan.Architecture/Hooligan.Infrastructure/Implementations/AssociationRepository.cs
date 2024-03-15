using Hooligan.Application.Interfaces;
using Hooligan.Domain;
using Hooligan.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Hooligan.Infrastructure.Implementations;

public class AssociationRepository(HooliganDbContext context, CraftableItems craftableItems) : IAssociationRepository
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
        var result = await craftableItems.ToListAsync(cancellationToken);
        return result.Contains(first) && result.Contains(second);
    }
}