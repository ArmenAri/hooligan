using Hooligan.Application.Interfaces;
using Hooligan.Application.Structures;
using Hooligan.Infrastructure.Clients;
using Hooligan.Infrastructure.Context;
using Hooligan.Infrastructure.Implementations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hooligan.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAssociationRepository, AssociationRepository>();

        services.AddScoped<CraftableItems>();

        services.AddScoped<EdenApiClient>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(NormalizationBehaviour<,>));

        services.AddKeyedScoped<IExternalAssociationProvider, FakerAssociationProvider>(ServiceKeys.Faker);
        services.AddKeyedScoped<IExternalAssociationProvider, EdenAssociationProvider>(ServiceKeys.Eden);

        return services.AddDbContext<HooliganDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("HooliganConnectionString"))
        );
    }
}