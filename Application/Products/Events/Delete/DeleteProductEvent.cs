using ErrorOr;
using MediatR;

namespace Application.Products.Commands.Delete;

/// <summary>
/// Command to delete a product.
/// </summary>
/// <param name="ProductId">Identifier of the product to delete</param>
public record DeleteProductEvent(Guid ProductId) : IRequest<ErrorOr<Success>>;