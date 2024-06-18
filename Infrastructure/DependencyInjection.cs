using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor()
            .AddServices()
            .AddAuthentication(configuration)
            .AddAuthorization()
            .AddPersistence();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        return services;
    }

    private static IServiceCollection AddAuthorization(this IServiceCollection services)
    {
        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        
        
        return services;
    }
}