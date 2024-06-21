using Application.Users.response;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.GetUser;

/// <summary>
/// Query to get a user by id.
/// </summary>
/// <param name="Id"> id of the user</param>
public record GetUserQuery(Guid Id): IRequest<ErrorOr<UserResponse>>;