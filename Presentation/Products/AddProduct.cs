namespace Presentation.Products;

/// <summary>
/// Represents a request to add a product.
/// </summary>
/// <param name="Name">Name of the product</param>
/// <param name="Code">Code of the product</param>
/// <param name="Description">Description of the product</param>
/// <param name="Price">Price of the product</param>
/// <param name="Stock">Stock of the product</param>
public record AddProductRequest(
    string Name,
    string Code,
    string Description,
    double Price,
    int Stock
    );