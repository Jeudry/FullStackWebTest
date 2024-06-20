using Application.Users.response;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.Profile;

/// <summary>
/// Get user profile by user id.
/// </summary>
/// <param name="UserId"> User id. </param>
public record GetProfileQuery(Guid UserId) : IRequest<ErrorOr<UserResponse>>;
