using System.Reflection;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    private const string SqlConnectionString = "SqlServerConnection";
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor()
            .AddServices()
            .AddAuthentication(configuration)
            .AddAuthorization()
            .AddPersistence(configuration);

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var migrationsAssembly = typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name;
        string? connectionString = configuration.GetConnectionString(SqlConnectionString);
        void ContextBuilder(DbContextOptionsBuilder b) =>
        	b.UseSqlServer(
        		connectionString,
        		sql =>
        		{
        			sql.MigrationsAssembly(migrationsAssembly);
        			sql.MigrationsHistoryTable(
        				"_EFAppDbMigrationHistory",
        				AppDbContext.DefaultSchema
        			);
        			sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        		}
        	);
        services.AddDbContext<AppDbContext>(ContextBuilder);
        services.AddScoped<DbContext, AppDbContext>();

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