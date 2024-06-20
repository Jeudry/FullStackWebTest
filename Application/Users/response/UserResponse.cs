namespace Application.Users.response;

/// <summary>
/// Response for the user
/// </summary>
/// <param name="UserName"> The user name</param>   
/// <param name="Email"> The user email</param>
public record UserResponse(string UserName, string Email);