using FullStackDevTest.Middleware;

namespace FullStackDevTest.extensions;

public static class AppExtensions
{
    public static void UseErrorHandlingMiddleWare(this IApplicationBuilder app)
    {
           app.UseMiddleware<ExceptionHandlingMiddleware>();  
    }
}