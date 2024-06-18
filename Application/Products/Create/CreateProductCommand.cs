using MediatR;

namespace Application.Products.Create;

/// <summary>
/// Command to create a product.
/// </summary>
/// <param name="Name">name of the product</param>
public record CreateProductCommand(string Name) : IRequest;