using Application.Products.Commands.Create;
using TestCommon.TestsConstants;

namespace TestCommon.Products;

/// <summary>
/// Factory for creating products commands.
/// </summary>
public sealed class ProductsCommandFactory
{
    public static CreateProductCommand CreateProductCommand(
        string name = Constants.Product.Text,
        string code = Constants.Product.Code,
        string description = Constants.Product.Description,
        double price = Constants.Product.Price,
        int stock = Constants.Product.Stock
        )
    {
        return new CreateProductCommand
        (
            name,
            code,
            description,
            price,
            stock
        );
    }
}