using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;

namespace FullStackDevTest;

public static class DependencyInjection
{
    private const string AllowOriginKey = "AllowOrigin";
    
    [RequiresUnreferencedCode("This method requires unreferenced code.")]
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwagger();
        services.AddProblemDetails();
        services.AddControllersConfig();

        return services;
    }

    [RequiresUnreferencedCode("Calls Microsoft.Extensions.DependencyInjection.MvcServiceCollectionExtensions.AddControllers()")]
    private static IServiceCollection AddControllersConfig(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(opt =>
            {

                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        
        services
            .AddControllers()
            .AddNewtonsoftJson(opts =>
            {
                opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft
                    .Json
                    .ReferenceLoopHandling
                    .Ignore;
                opts.SerializerSettings.NullValueHandling = Newtonsoft
                    .Json
                    .NullValueHandling
                    .Ignore;
                opts.SerializerSettings.DateTimeZoneHandling = Newtonsoft
                    .Json
                    .DateTimeZoneHandling
                    .Local;
                opts.UseCamelCasing(false);
            });
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
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

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
}