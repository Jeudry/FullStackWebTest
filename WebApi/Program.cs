using Application;
using FullStackDevTest;
using FullStackDevTest.extensions;
using FullStackDevTest.Middleware;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;

const string OriginsKey = "Origins";
const string AllowOriginKey = "AllowOrigin";
const string ApplyMigrationsKey = "ApplyMigrations";


var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation(builder.Configuration).AddApplication().AddInfrastructure(builder.Configuration);
}
var origins = builder.Configuration.GetSection(OriginsKey).Get<string[]>();

builder.Services.AddCors(
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
var applyMigrations = builder.Configuration[ApplyMigrationsKey];


var app = builder.Build();
app.UseRouting();
AddOrigins(app, builder.Configuration);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseErrorHandlingMiddleWare();
app.UseEndpoints(endpoints => endpoints.MapControllers());

if (applyMigrations == "true")
{
    using var serviceScope = app.Services.CreateScope();
    using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
    context.Database.Migrate();
}


app.Run();

static void AddOrigins(WebApplication app, IConfiguration configuration)
{
    var origins = configuration.GetSection(OriginsKey).Get<string[]>();
    app.UseCors(builder =>
        builder.WithOrigins(origins)
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod()

            .SetIsOriginAllowed(origin => true));
}