using Application;
using FullStackDevTest;
using Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation().AddApplication().AddInfrastructure(builder.Configuration);
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();