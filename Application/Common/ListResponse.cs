namespace Application.Common;

/// <summary>
/// Represents a list response.
/// </summary>
/// <param name="Items"> List of items </param>
/// <param name="Total"> Total number of items </param>
/// <typeparam name="T"> Type of the items </typeparam>
public record ListResponse<T>(
    IList<T> Items,
    int Total
);