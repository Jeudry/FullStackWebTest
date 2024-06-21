using System.Windows.Input;

namespace Presentation.Products;

/// <summary>
/// Represents a request to update a product.
/// </summary>
/// <param name="Id"> id of the product </param>
/// <param name="Name"> name of the product </param>
/// <param name="Price"> price of the product </param>
/// <param name="Stock"> product available quantity </param>
/// <param name="CreatedAt"> created at date </param>
/// <param name="Description"> description of the product </param>
/// <param name="UpdatedAt"> updated at date </param>
public sealed record UpdateProductRequest(Guid Id, string Name, double Price, int Stock, DateTime CreatedAt, string? Description = null, DateTime? UpdatedAt = null);