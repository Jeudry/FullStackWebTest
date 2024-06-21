namespace Presentation.Users;

/// <summary>
/// Login user request
/// </summary>
/// <param name="UserName"> name of the user</param>
/// <param name="Password"> password of the user</param>
public record LoginUserRequest(string UserName, string Password);