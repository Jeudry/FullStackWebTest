using ErrorOr;
using MediatR;

namespace Application.Users.Commands.Create;

/// <summary>
/// Command to create a user.
/// </summary>
/// <param name="UserName"> name of the user</param>
/// <param name="Email">email of the user</param>
/// <param name="Password">password of the user</param>
/// <param name="ConfirmPassword">confirm password of the user</param>
/// <param name="RolesId">role id of the user</param>
/// <param name="Id">id of the user</param>
public record CreateUserCommand(string UserName, string Email, string Password, string ConfirmPassword, List<string> RolesId, Guid? Id = null) : IRequest<ErrorOr<Success>>;