namespace Application.Users.response;

/// <summary>
/// Response for the user
/// </summary>
/// <param name="id"> The user id</param>
/// <param name="UserName"> The user name</param>   
/// <param name="Email"> The user email</param>
public record UserResponse(string id, string UserName, string Email, List<RoleResponse> Roles);