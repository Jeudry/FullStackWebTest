using Application;
using FullStackDevTest;
using FullStackDevTest.Middleware;
using Infrastructure;
using Serilog;

const string OriginsKey = "Origins";
const string AllowOriginKey = "AllowOrigin";


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
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseEndpoints(endpoints => endpoints.MapControllers());


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