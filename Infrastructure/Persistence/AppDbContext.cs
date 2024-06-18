using Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

/// <summary>
/// Represents the application database context.
/// </summary>
/// <param name="options"> options for the database context </param>
/// <param name="_httpContextAccessor"> http context accessor </param>
/// <param name="_publisher"> mediator publisher </param>
public class AppDbContext(DbContextOptions options, IHttpContextAccessor _httpContextAccessor, IPublisher _publisher): DbContext(options)
{
    /// <summary>
    /// Represents the products table.
    /// </summary>
    public DbSet<Product> Products { get; set; } = null!;

    /// <summary>
    /// Save changes async method override.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}