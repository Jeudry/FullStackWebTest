namespace Application.Products.Responses;

/// <summary>
/// Product response
/// </summary>
/// <param name="Id"> Identifier of the product </param>
/// <param name="Name"> Name of the product </param>
/// <param name="Code"> Code of the product </param>
/// <param name="Description"> Description of the product </param>
/// <param name="Price"> Price of the product </param>
/// <param name="Stock"> Stock of the product </param>
/// <param name="CreatedAt"> Date of creation of the product </param>
/// <param name="UpdatedAt"> Date of the last update of the product </param>
public record ProductResponse(
    Guid Id,
    string Name,
    string Code,
    string Description,
    double Price,
    int Stock,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);