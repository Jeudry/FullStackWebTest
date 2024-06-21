namespace Presentation.Users;

/// <summary>
/// Create User Request
/// </summary>
/// <param name="UserName"></param>
/// <param name="Email"></param>
/// <param name="Password"></param>
/// <param name="ConfirmPassword"></param>
/// <param name="RolesId"></param>
/// <param name="Id"></param>
public record CreateUserRequest(
    string UserName, string Email, string Password, string ConfirmPassword, List<string> RolesId, Guid? Id = null);