using System.Net;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FullStackDevTest.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception ocurred: {Message}", ex.Message);
            
            var exceptionDetails = GetExceptionDetails(ex);

            var problemDetails = new ProblemDetails
            {
                Title = exceptionDetails.Title,
                Status = exceptionDetails.Status,
                Detail = exceptionDetails.Detail,
                Type = exceptionDetails.Type
            };
            
            if(exceptionDetails.Errors is not null)
            {
                problemDetails.Extensions.Add("errors", exceptionDetails.Errors);
            }
            
            
            context.Response.StatusCode = exceptionDetails.Status;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
    
    private static ExceptionDetails GetExceptionDetails(Exception ex)
    {
        return ex switch
        {
            ValidationException validationException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "ValidationFailure",
                "Validation error",
                "One or more validation errors occurred",
                validationException.Errors),
            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "ServerError",
                "Server error",
                "An unexpected error occurred",
                null
                )
        };
    }

    internal record ExceptionDetails(
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors);
}