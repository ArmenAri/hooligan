using Hooligan.Domain;
using LanguageExt.Common;

namespace Hooligan.Application.Interfaces;

public interface IExternalAssociationProvider
{
    public Task<Result<Association>> GetNewAsync(string first, string second,
        CancellationToken cancellationToken = default);
}