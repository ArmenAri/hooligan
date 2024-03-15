using Hooligan.Application.Interfaces;
using MediatR;

namespace Hooligan.Infrastructure.Implementations;

public class NormalizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is null or not INormalizeProperties)
        {
            return next();
        }

        var properties = request.GetType().GetProperties();

        foreach (var property in properties)
        {
            if (property.PropertyType != typeof(string))
            {
                continue;
            }

            var value = property.GetValue(request) as string;

            if (!string.IsNullOrEmpty(value))
            {
                property.SetValue(request, value.ToLowerInvariant());
            }
        }

        return next();
    }
}
