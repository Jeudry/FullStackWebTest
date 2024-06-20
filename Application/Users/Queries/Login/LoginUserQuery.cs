using Application.Users.response;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.Login;

public record LoginUserQuery(string UserName, string Password) : IRequest<ErrorOr<LoginResponse>>;
