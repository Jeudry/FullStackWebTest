using Application.Products.Commands.Create;
using Application.Products.Create;
using TestCommon.TestsConstants;

namespace TestCommon.Products;

public class ProductsCommandFactory
{
    public static CreateProductCommand CreateProductCommand(
        string name = Constants.Product.Text,
        string code = Constants.Product.Code
        )
    {
        return new CreateProductCommand
        (
            name,
            code
        );
    }
}