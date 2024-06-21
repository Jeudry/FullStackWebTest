namespace Presentation.Users;

/// <summary>
/// Update User Request
/// </summary>
/// <param name="UserName"></param>
/// <param name="Email"></param>
/// <param name="RolesId"></param>
/// <param name="Id"></param>
public record UpdateUserRequest(string UserName, string Email, Guid RolesId, Guid? Id = null);