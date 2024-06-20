using Application.Products.Responses;
using Domain.Product;
using ErrorOr;
using MediatR;

namespace Application.Products.Queries.GetProduct;

/// <summary>
/// Query to get a product by id.
/// </summary>
/// <param name="Id"> id of the product</param>
public record GetProductQuery(Guid Id): IRequest<ErrorOr<ProductResponse>>;