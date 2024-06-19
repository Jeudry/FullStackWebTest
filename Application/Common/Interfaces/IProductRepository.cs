using Domain.Product;

namespace Application.Common.Interfaces;

/// <summary>
/// Interface for the product repository.
/// </summary>
public interface IProductRepository
{ 
    Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken);
    
    Task<bool> IsProductUniqueAsync(string code, CancellationToken cancellationToken);
    
    Task AddAsync(Product product, CancellationToken cancellationToken);
}