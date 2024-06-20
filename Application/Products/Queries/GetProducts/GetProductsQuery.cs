using Domain.Product;
using ErrorOr;
using MediatR;

namespace Application.Products.Queries.GetProducts;

/// <summary>
/// Query to get all products.
/// </summary>
public record GetProductsQuery: IRequest<ErrorOr<List<Product>>>;