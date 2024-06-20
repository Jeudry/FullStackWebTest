using FullStackDevTest;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure.Tests.Common;

public class WebAppFactory: WebApplicationFactory<IAssemblyMarker>, IAsyncLifetime
{
    
    public SqlTestDb TestDatabase { get; set; } = null!;
    
    public IMediator CreateMediator()
    {
        var serviceScope = Services.CreateScope();
        
        TestDatabase.ResetDatabase();
        
        return serviceScope.ServiceProvider.GetRequiredService<IMediator>();
    }

    public Task InitializeAsync() => Task.CompletedTask;
    

    public Task DisposeAsync()
    {
        TestDatabase.Dispose();
            
        return Task.CompletedTask;
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        TestDatabase = SqlTestDb.CreateAndInitialize();
        
        builder.ConfigureTestServices(services =>
        {
            services
                .RemoveAll<DbContextOptions<AppDbContext>>()
                .AddDbContext<AppDbContext>((sp, options) => options.UseInMemoryDatabase(TestDatabase.Connection.ConnectionString))
                .AddScoped<DbContext, AppDbContext>();
        });
    }
}