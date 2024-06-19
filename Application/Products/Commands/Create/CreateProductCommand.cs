using ErrorOr;
using MediatR;

namespace Application.Products.Commands.Create;

using Application.Common;

/// <summary>
/// Command to create a product.
/// </summary>
/// <param name="Name">name of the product</param>
public record CreateProductCommand(string Name, string Code) : IRequest<ErrorOr<Success>>;

/// <summary>
/// Request to create a product.
/// </summary>
/// <param name="Name">name of the product</param>
public record CreateProductRequest(string Name);