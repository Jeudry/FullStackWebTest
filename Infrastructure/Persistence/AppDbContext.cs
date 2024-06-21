using Domain.Product;
using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

/// <summary>
/// Represents the application database context.
/// </summary>
/// <param name="options"> options for the database context </param>
/// <param name="_httpContextAccessor"> http context accessor </param>
/// <param name="_publisher"> mediator publisher </param>
public sealed class AppDbContext: IdentityDbContext<User>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPublisher _publisher;
    
    public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor, IPublisher publisher) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        _publisher = publisher;
        Products = Set<Product>();
    }
    
    /// <summary>
    /// Represents the default schema of the database.
    /// </summary>
    public const string DefaultSchema = "AppDb";
    
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
        modelBuilder.HasDefaultSchema(DefaultSchema);
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("UserRoles");
            entity.HasKey(e => new { e.UserId, e.RoleId });
            entity.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "dc043262-673a-491a-b811-446703743743",
                    UserId = "dc043262-673a-491a-b811-446703743743"
                }
            );
        });

        base.OnModelCreating(modelBuilder);
    }
}