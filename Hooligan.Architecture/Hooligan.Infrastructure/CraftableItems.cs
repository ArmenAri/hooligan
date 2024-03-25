using System.Collections;
using System.Linq.Expressions;
using Hooligan.Infrastructure.Context;

namespace Hooligan.Infrastructure;

public class CraftableItems : IQueryable<string>, IAsyncEnumerable<string>
{
    private readonly IQueryable<string> _queryableItems;

    public CraftableItems(HooliganDbContext context)
    {
        _queryableItems = context.Associations.Select(item => item.Result)
            .Distinct()
            .Concat(OriginalItems);
    }

    public Type ElementType => typeof(string);

    public Expression Expression => _queryableItems.Expression;

    public IQueryProvider Provider => _queryableItems.Provider;

    private static readonly string[] OriginalItems =
    [
        "Earth",
        "Water",
        "Wind",
        "Fire"
    ];

    public IEnumerator<string> GetEnumerator()
    {
        return _queryableItems.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IAsyncEnumerator<string> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return ((IAsyncEnumerable<string>)_queryableItems).GetAsyncEnumerator(cancellationToken);
    }
}