using Microsoft.AspNetCore.Identity;

namespace Domain.User;

/// <summary>
/// Represents the user entity.
/// </summary>
public sealed class User : IdentityUser
{
    
    public const string DefaultRole = "User";
    public const string AdminRole = "Admin";
    
    public const int MaxPasswordLength = 255;
    public const int MinPasswordLength = 8; 
    
    public const int MaxUsernameLength = 256;
    public const int MinUsernameLength = 3;
    
    public const int MaxEmailLength = 256;
    public const int MinEmailLength = 3;
}