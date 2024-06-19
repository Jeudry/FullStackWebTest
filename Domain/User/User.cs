using Microsoft.AspNetCore.Identity;

namespace Domain.User;

/// <summary>
/// Represents the user entity.
/// </summary>
public sealed class User(string name) : IdentityUser
{
    /// <summary>
    /// Represents a username.
    /// </summary>
    public string Name { get; } = name;
    
    /// <summary>
    /// Represents the first name of the user.
    /// </summary>
    public string LastName { get; set; }
}