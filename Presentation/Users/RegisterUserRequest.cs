namespace Presentation.Users;

/// <summary>
/// Register user request
/// </summary>
/// <param name="UserName"> name of the user</param>
/// <param name="Email"> email of the user</param>
/// <param name="Password"> password of the user</param>
/// <param name="ConfirmPassword"> confirm password of the user</param>
/// <param name="Id"> id of the user</param>
public record RegisterUserRequest(
    string UserName, string Email, string Password, string ConfirmPassword, Guid? Id = null
    );