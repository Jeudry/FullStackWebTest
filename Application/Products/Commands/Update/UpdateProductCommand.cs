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
/// <param name="CreatedAt">created at date</param>
/// <param name="UpdatedAt">updated at date</param>
public record UpdateProductCommand(Guid Id, string Name, string Code, double Price, int Stock, DateTime CreatedAt, string? Description, DateTime? UpdatedAt = null) : IRequest<ErrorOr<Success>>;