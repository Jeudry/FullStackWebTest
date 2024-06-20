using Application;
using FullStackDevTest;
using FullStackDevTest.Middleware;
using Infrastructure;
using Serilog;

const string OriginsKey = "Origins";

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation(builder.Configuration).AddApplication().AddInfrastructure(builder.Configuration);
}

var app = builder.Build();

AddOrigins(app, builder.Configuration);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();

static void AddOrigins(WebApplication app, IConfiguration configuration)
{
    var origins = configuration.GetSection(OriginsKey).Get<string[]>();
    app.UseCors(builder =>
        builder.WithOrigins(origins)
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod());
}