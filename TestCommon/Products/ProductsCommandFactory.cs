using Application.Products.Commands.Create;
using Application.Products.Commands.Delete;
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
        int stock = Constants.Product.Stock,
        Guid? id = null
        )
    {
        return new CreateProductCommand
        (
            name,
            code,
            description,
            price,
            stock,
            id ?? Constants.Product.Id
        );
    }
    
    public static DeleteProductCommand DeleteProductCommand(
        Guid? productId = null
        )
    {
        return new DeleteProductCommand(
            productId ?? Constants.Product.Id
            );
    }
}