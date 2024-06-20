using Domain.Product;

namespace Application.Common.Interfaces;

/// <summary>
/// Represents the product repository interface.
/// </summary>
public interface IProductRepository
{ 
    /// <summary>
    /// Get a product by its id.
    /// </summary>
    /// <param name="productId">Identifies the product to delete.</param>
    /// <param name="cancellationToken"> Propagates notification that operations should be canceled.</param>
    /// <returns>The Product Entity</returns>
    Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Check if a product code is unique.
    /// </summary> 
    /// <param name="code"> The product code to check.</param>
    /// <param name="cancellationToken"> Propagates notification that operations should be canceled.</param>
    /// <returns> Indicates if the product code is unique.</returns>
    Task<bool> IsProductCodeUniqueAsync(string code, CancellationToken cancellationToken);
    
    /// <summary>
    /// Add a product to the repository.
    /// </summary>
    /// <param name="product"> The product to add.</param>
    /// <param name="cancellationToken"> Propagates notification that operations should be canceled.</param>
    /// <returns>The task result</returns>
    Task AddAsync(Product product, CancellationToken cancellationToken);
    
    /// <summary>
    /// Deletes a product from the repository.
    /// </summary>
    /// <param name="product">The product to delete.</param>
    /// <returns>The task result</returns>
    void DeleteAsync(Product product);
}