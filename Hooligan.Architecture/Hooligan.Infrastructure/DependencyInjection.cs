using Hooligan.Application.Interfaces;
using Hooligan.Infrastructure.Context;
using Hooligan.Infrastructure.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hooligan.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAssociationRepository, AssociationRepository>();
        services.AddScoped<IExternalAssociationProvider, FakerAssociationProvider>();

        return services.AddDbContext<HooliganDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("HooliganConnectionString"))
        );
    }
}