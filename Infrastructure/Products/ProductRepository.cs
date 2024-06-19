using Application.Common.Interfaces;
using Domain.Product;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Products;

internal sealed class ProductRepository(AppDbContext context) : IProductRepository
{
    public Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsProductUniqueAsync(string code, CancellationToken cancellationToken)
    {
        return await context.Products.AnyAsync(p => p.Code == code, cancellationToken);
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}