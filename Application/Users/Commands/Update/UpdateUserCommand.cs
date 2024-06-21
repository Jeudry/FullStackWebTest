using ErrorOr;
using MediatR;

namespace Application.Users.Commands.Update;

/// <summary>
/// Command to update a user.
/// </summary>
/// <param name="UserName"> name of the user</param>
/// <param name="Email">email of the user</param>
/// <param name="RoleId">role id of the user</param>
/// <param name="Id">id of the user</param>
public record UpdateUserCommand(string UserName, string Email, Guid RoleId, Guid? Id = null) : IRequest<ErrorOr<Success>>;