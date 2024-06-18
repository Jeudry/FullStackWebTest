using Domain.Products;

namespace Application.Common.Interfaces;

/// <summary>
/// Interface for the product repository.
/// </summary>
public interface IProductRepository
{
    Task AddAsync(Product product, CancellationToken cancellationToken);
}