namespace Application.Users.response;

public record LoginResponse(
    bool IsAuthSuccessful,
    string? Expiration,
    int? Error,
    string? Token,
    string? Message
    );