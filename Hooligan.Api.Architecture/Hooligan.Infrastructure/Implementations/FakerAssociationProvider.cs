using Bogus;
using Hooligan.Application.Interfaces;
using Hooligan.Domain;

namespace Hooligan.Infrastructure.Implementations;

public class FakerAssociationProvider : IExternalAssociationProvider
{
    private readonly Func<string, string, Faker<Association>> _fake = (first, second) => new Faker<Association>()
        .RuleFor(a => a.First, _ => first)
        .RuleFor(a => a.Second, _ => second)
        .RuleFor(a => a.Result, f => f.Internet.UserName())
        .RuleFor(a => a.Icon, _ => "😀");

    public Task<Association> GetNew(string first, string second, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_fake(first, second).Generate());
    }
}