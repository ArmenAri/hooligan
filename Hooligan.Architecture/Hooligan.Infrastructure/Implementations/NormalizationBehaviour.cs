using Hooligan.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hooligan.Infrastructure.Implementations;

public class NormalizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not null and INormalizeProperties)
        {
            var properties = request.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    var value = property.GetValue(request) as string;
                    if (!string.IsNullOrEmpty(value))
                    {
                        property.SetValue(request, value.ToLowerInvariant());
                    }
                }
            }
        }

        return next();
    }
}
