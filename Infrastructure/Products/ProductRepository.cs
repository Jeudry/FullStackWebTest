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
    /// see <see cref="IProductRepository.IsProductNameUniqueAsync"/>
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> IsProductNameUniqueAsync(string name, CancellationToken cancellationToken)
    {
        return !await context.Products.AnyAsync(p => p.Name == name, cancellationToken);
    }

    /// <summary>
    /// see <see cref="IProductRepository.IsProductNameUniqueAsync(string,System.Guid,System.Threading.CancellationToken)"/>
    /// </summary>
    /// <param name="name"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> IsProductNameUniqueAsync(string name, Guid id, CancellationToken cancellationToken)
    {
        return !await context.Products.AnyAsync(p => p.Name == name && p.Id != id, cancellationToken);
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
    /// <param name="sortBy"></param>
    /// <param name="direction"></param>
    /// <param name="search"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public async Task<List<Product>> GetAllAsync(string sortBy, string direction, int limit, int offset, string? search, CancellationToken cancellationToken)
    {
        var query = context.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.Name.Contains(search) || (p.Description != null && p.Description.Contains(search)));
        }
        query = sortBy switch
        {
            "name" => direction == "asc" ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name),
            "price" => direction == "asc" ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price),
            "stock" => direction == "asc" ? query.OrderBy(p => p.Stock) : query.OrderByDescending(p => p.Stock),
            "createdAt" => direction == "asc" ? query.OrderBy(p => p.CreatedAt) : query.OrderByDescending(p => p.CreatedAt),
            "updatedAt" => direction == "asc" ? query.OrderBy(p => p.UpdatedAt) : query.OrderByDescending(p => p.UpdatedAt),
            _ => query.OrderBy(p => p.CreatedAt)
        };
        return await query.Skip(offset).Take(limit).ToListAsync(cancellationToken);
    }

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

    /// <summary>
    /// see <see cref="IProductRepository.GetTotalCountAsync"/>
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int> GetTotalCountAsync(CancellationToken cancellationToken)
    {
        return context.Products.CountAsync(cancellationToken);
    }
}