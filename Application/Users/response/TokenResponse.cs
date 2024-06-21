namespace Application.Users.response;

/// <summary>
/// Response for the Token.
/// </summary>
/// <param name="Token"> The token.</param>
/// <param name="Expiration"> The expiration.</param>
public record TokenResponse(
    string Token,
    string Expiration
    );