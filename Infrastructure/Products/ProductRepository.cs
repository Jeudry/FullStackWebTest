using Application.Common.Interfaces;
using Domain.Products;
using Infrastructure.Persistence;

namespace Infrastructure.Products;

internal sealed class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}