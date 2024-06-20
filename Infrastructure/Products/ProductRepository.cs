using Application.Common.Interfaces;
using Domain.Product;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Products;

internal sealed class ProductRepository(AppDbContext context) : IProductRepository
{
    /// <summary>
    /// see <see cref="IProductRepository.GetByIdAsync"/>
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        return await context.Products.FindAsync(new object[] { productId }, cancellationToken);
    }

    public async Task<bool> IsProductCodeUniqueAsync(string code, CancellationToken cancellationToken)
    {
        return await context.Products.AnyAsync(p => p.Code == code, cancellationToken);
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public void DeleteAsync(Product product)
    {
        context.Products.Remove(product);
        context.SaveChanges();
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Products.ToListAsync(cancellationToken);
    }
}