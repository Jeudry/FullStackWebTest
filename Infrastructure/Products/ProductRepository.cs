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
        return await context.Products.FirstOrDefaultAsync(product => product.Id == productId, cancellationToken);
    }

    /// <summary>
    /// see <see cref="IProductRepository.IsProductCodeUniqueAsync(string,System.Threading.CancellationToken)"/>
    /// </summary>
    /// <param name="code"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> IsProductCodeUniqueAsync(string code, CancellationToken cancellationToken)
    {
        return await context.Products.AnyAsync(p => p.Code == code, cancellationToken);
    }

    /// <summary>
    /// see <see cref="IProductRepository.IsProductCodeUniqueAsync(string,System.Guid,System.Threading.CancellationToken)"/>
    /// </summary>
    /// <param name="code"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> IsProductCodeUniqueAsync(string code, Guid id, CancellationToken cancellationToken)
    {
        return await context.Products.AnyAsync(p => p.Code == code && p.Id != id, cancellationToken);
    }

    /// <summary>
    /// see <see cref="IProductRepository.AddAsync"/>
    /// </summary>
    /// <param name="product"></param>
    /// <param name="cancellationToken"></param>
    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// see <see cref="IProductRepository.DeleteAsync"/>
    /// </summary>
    /// <param name="product"></param>
    public void DeleteAsync(Product product)
    {
        context.Products.Remove(product);
        context.SaveChanges();
    }

    /// <summary>
    /// see <see cref="IProductRepository.GetAllAsync"/>
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
    => await context.Products.ToListAsync(cancellationToken);
    
    /// <summary>
    /// see <see cref="IProductRepository.UpdateAsync"/>
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public void UpdateAsync(Product product)
    {
        context.Products.Update(product);
        context.SaveChanges();
    }
}