using System.Reflection;
using System.Text;
using Application.Common.Interfaces;
using Domain.User;
using Infrastructure.Helpers;
using Infrastructure.Persistence;
using Infrastructure.Products;
using Infrastructure.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class DependencyInjection
{
    private const string SqlConnectionString = "SqlServerConnection";
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextServices()
            .AddJwtToken(configuration)
            .AddServices()
            .AddRepositories()
            .AddAuth()
            .AddPersistence(configuration);

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
    
    private static IServiceCollection AddJwtToken(this IServiceCollection services, IConfiguration configuration)
    {
        
        IdentityConfig identityConfig = new(configuration);
        
        services
            .AddAuthentication(options =>
            {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = identityConfig.ValidAudience,
                    ValidIssuer = identityConfig.ValidIssuer,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(identityConfig.Secret)
                    ),
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });
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

    private static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddAuthorization();
        services
            .AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = User.MinPasswordLength;
                opts.Password.RequireDigit = true;
                opts.Password.RequiredUniqueChars = 0;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.SignIn.RequireConfirmedEmail = false;
                opts.User.RequireUniqueEmail = true;
                opts.Lockout.AllowedForNewUsers = true;
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opts.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(
            opt => opt.TokenLifespan = TimeSpan.FromHours(2)
        );

        
        return services;
    }

    private static IServiceCollection AddHttpContextServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddHttpClient();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }
}