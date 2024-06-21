namespace Application.Users.response;

/// <summary>
/// Represents a role response.
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
public record RoleResponse(
    string Id,
    string Name
    );