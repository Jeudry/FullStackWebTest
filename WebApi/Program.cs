using Application;
using FullStackDevTest;
using FullStackDevTest.Middleware;
using Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation(builder.Configuration).AddApplication().AddInfrastructure(builder.Configuration);
}

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();