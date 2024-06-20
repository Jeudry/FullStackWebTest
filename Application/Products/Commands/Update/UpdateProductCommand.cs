using ErrorOr;
using MediatR;

namespace Application.Products.Commands.Update;

/// <summary>
/// Command to update a product.
/// </summary>
/// <param name="Name">name of the product</param>
/// <param name="Code">code of the product</param>
/// <param name="Description">description of the product</param>
/// <param name="Price">price of the product</param>
/// <param name="Stock">product available quantity</param>
/// <param name="Id">id of the product</param>
public record UpdateProductCommand(string Name, string Code, string Description, double Price, int Stock, Guid? Id = null) : IRequest<ErrorOr<Success>>;