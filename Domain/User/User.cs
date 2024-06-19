using Microsoft.AspNetCore.Identity;

namespace Domain.User;

/// <summary>
/// Represents the user entity.
/// </summary>
public sealed class User(string name, string lastName) : IdentityUser
{
    /// <summary>
    /// Represents first the name of the user.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Represents the last name of the user.
    /// </summary>
    public string LastName { get; set; } = lastName;

    /// <summary>
    /// Represents t
    /// </summary>
    public DateTime BirthDate { get; set; }
}