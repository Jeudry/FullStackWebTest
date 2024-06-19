using Microsoft.Extensions.Configuration;

namespace Infrastructure.Helpers;

public sealed class IdentityConfig
{
    internal IdentityConfig(IConfiguration configuration)
    {
        Secret = configuration["JWT:Secret"]!;
        ValidIssuer = configuration["JWT:ValidIssuer"]!;
        ValidAudience = configuration["JWT:ValidAudience"]!;
        AccessTokenExpiration = int.Parse(configuration["JWT:AccessTokenExpiration"]!);
        RefreshTokenExpiration = int.Parse(configuration["JWT:RefreshTokenExpiration"]!);
    }
    
    /// <summary>
    /// Represents a valid authority.
    /// </summary>
    public string Secret { get; }
    
    /// <summary>
    /// Represents a valid jwt url creator.
    /// </summary>
    public string ValidIssuer { get; }
    /// <summary>
    /// Represents a valid jwt url audience.
    /// </summary>
    public string ValidAudience { get; }
    /// <summary>
    /// Represents a valid jwt access token expiration.
    /// </summary>
    public int AccessTokenExpiration { get; }
    /// <summary>
    /// Represents a valid jwt refresh token expiration.
    /// </summary>
    public int RefreshTokenExpiration { get; }
}