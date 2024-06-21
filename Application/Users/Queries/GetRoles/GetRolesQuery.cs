using Application.Users.response;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.GetRoles;

/// <summary>
/// Represents a query to get roles.
/// </summary>
public record GetRolesQuery(): IRequest<ErrorOr<List<RoleResponse>>>;