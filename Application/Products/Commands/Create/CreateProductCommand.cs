using ErrorOr;
using MediatR;

namespace Application.Products.Commands.Create;

/// <summary>
/// Command to create a product.
/// </summary>
/// <param name="Name">name of the product</param>
/// <param name="Code">code of the product</param>
/// <param name="Description">description of the product</param>
/// <param name="Price">price of the product</param>
/// <param name="Stock">product available quantity</param>
/// <param name="Id">id of the product if its needed</param>
public record CreateProductCommand(string Name, string Code, string Description, double Price, int Stock, Guid? Id = null) : IRequest<ErrorOr<Success>>;

/// <summary>
/// Request to create a product.
/// </summary>
/// <param name="Name">name of the product</param>
public record CreateProductRequest(string Name);