using Hooligan.Application;
using Hooligan.Infrastructure;

namespace Hooligan.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddInfrastructure(configuration).AddApplication();
    }
}