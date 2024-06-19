using Domain.Product;
using TestCommon.TestsConstants;


namespace TestCommon.Products;

public static class ProductsFactory
{
    public static Product CreateProduct(
        Guid? id = null,
        string name = Constants.Product.Text,
        string code = Constants.Product.Code
        )
    {
        return new Product(
            id ?? Constants.Product.Id,
            name,
            code
            );
    }
}