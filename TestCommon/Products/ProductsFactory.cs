using Domain.Product;
using TestCommon.TestsConstants;

namespace TestCommon.Products;

/// <summary>
/// Factory for creating products.
/// </summary>
public static class ProductsFactory
{
    public static Product CreateProduct(
        Guid? id = null,
        string name = Constants.Product.Text,
        string code = Constants.Product.Code,
        string description = Constants.Product.Description,
        double price = Constants.Product.Price,
        int stock = Constants.Product.Stock,
        DateTime? createdAt = null,
        DateTime? updatedAt = null
        )
    {
        return new Product(
            id ?? Constants.Product.Id,
            name,
            code,
            description,
            price,
            stock,
            createdAt ?? Constants.Product.CreatedAt,
            updatedAt
            );
    }
}