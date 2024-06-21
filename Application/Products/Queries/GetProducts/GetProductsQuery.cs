using Application.Common;
using Application.Products.Responses;
using Domain.Product;
using ErrorOr;
using MediatR;

namespace Application.Products.Queries.GetProducts;

/// <summary>
/// Represents a query to get products.
/// </summary>
/// <param name="SortBy"> sort by field </param>
/// <param name="Direction"> sort direction </param>
/// <param name="Limit"> limit of products to return </param>
/// <param name="Offset"> offset of products to return </param>
/// <param name="Search"> search term to filter products </param>
public record GetProductsQuery(
    string SortBy,
    string Direction,
    int Limit,
    int Offset,
    string? Search
    ): IRequest<ErrorOr<ListResponse<ProductResponse>>>;