using Hooligan.Domain.OriginalsItems;
using Microsoft.Extensions.DependencyInjection;

namespace Hooligan.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(IAnchor)));
        services.AddSingleton<OriginalItems>();
        return services;
    }
}