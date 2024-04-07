using Bogus;
using Hooligan.Application.Interfaces;
using Hooligan.Common.Extensions;
using Hooligan.Domain;
using LanguageExt.Common;

namespace Hooligan.Infrastructure.Implementations;

public sealed class FakerAssociationProvider : IExternalAssociationProvider
{
    private readonly Func<string, string, Faker<Association>> _fake = (first, second) => new Faker<Association>()
        .RuleFor(a => a.First, _ => first)
        .RuleFor(a => a.Second, _ => second)
        .RuleFor(a => a.Result, f => f.Internet.UserName().ToUpperOnlyFirstCharacterInvariant())
        .RuleFor(a => a.Icon, _ => "😀");

    public Task<Result<Association>> GetNewAsync(string first, string second,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult<Result<Association>>(_fake(first, second).Generate());
    }
}