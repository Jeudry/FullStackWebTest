using Application.Common;
using Application.Products.Responses;
using Application.Users.response;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.GetProducts;

/// <summary>
/// Represents a query to get users.
/// </summary>
/// <param name="SortBy"> sort by field </param>
/// <param name="Direction"> sort direction </param>
/// <param name="Limit"> limit of users to return </param>
/// <param name="Offset"> offset of users to return </param>
/// <param name="Search"> search term to filter products </param>
public record GetUsersQuery(
    string SortBy,
    string Direction,
    int Limit,
    int Offset,
    string? Search
    ): IRequest<ErrorOr<ListResponse<UserResponse>>>;