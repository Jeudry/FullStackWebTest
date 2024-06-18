using System.Diagnostics.CodeAnalysis;

namespace FullStackDevTest;

public static class DependencyInjection
{
    [RequiresUnreferencedCode("This method requires unreferenced code.")]
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddProblemDetails();

        return services;
    }
}