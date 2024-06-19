using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;

namespace FullStackDevTest;

public static class DependencyInjection
{
    private const string AllowOriginKey = "AllowOrigin";
    private const string OriginsKey = "Origins";
    
    [RequiresUnreferencedCode("This method requires unreferenced code.")]
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwagger();
        services.AddProblemDetails();
        services.AddOrigins(configuration);

        return services;
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "FullStack Module API",
                    Version = "v1",
                    Description = "FullStack Module",
                    Contact = new OpenApiContact
                    {
                        Name = "Jeudry Abdiel Pena Pena",
                        Email = "jeudrypp@gmail.com"
                    }
                }
            );

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(
                new OpenApiSecurityRequirement { { securityScheme, Array.Empty<string>() } }
            );
        });
        return services;
    }
    
    private static IServiceCollection AddOrigins(this IServiceCollection services, IConfiguration configuration)
    {
        var origins = configuration.GetSection(OriginsKey).Get<string[]>()!;

        services.AddCors(
            c =>
                c.AddPolicy(AllowOriginKey,
                    builder =>
                        builder
                            .WithOrigins(origins)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed(origin => true)
                )
        );
        return services;
    }
}