using Application.Products.Commands.Create;
using Application.Products.Commands.Delete;
using Application.Products.Queries.GetProduct;
using Application.Products.Queries.GetProducts;
using TestCommon.TestsConstants;

namespace TestCommon.Products;

/// <summary>
/// Factory for creating products commands.
/// </summary>
public sealed class ProductsCommandFactory
{
    /// <summary>
    /// Create a new product command.
    /// </summary>
    /// <param name="name"> The name of the product. </param>
    /// <param name="code"> The code of the product. </param>
    /// <param name="description"> The description of the product. </param> 
    /// <param name="price"> The price of the product. </param>
    /// <param name="stock"> The stock of the product. </param>
    /// <param name="id"> The id of the product. </param>
    /// <returns> Create product command. </returns>
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
    
    /// <summary>
    /// Create a new delete product command.
    /// </summary>
    /// <param name="productId"> The id of the product. </param>
    /// <returns> Delete product command. </returns>
    public static DeleteProductEvent DeleteProductEvent(
        Guid? productId = null
        )
    {
        return new DeleteProductEvent(
            productId ?? Constants.Product.Id
            );
    }
    
    /// <summary>
    /// Create a new get product query.
    /// </summary>
    /// <param name="productId"> The id of the product. </param>
    /// <returns> Gets product query. </returns>
    public static GetProductQuery GetProductQuery (
        Guid? productId = null
        )
    {
        return new GetProductQuery(
            productId ?? Constants.Product.Id
            );
    }
    
    /// <summary>
    /// Create a new get products query.
    /// </summary>
    /// <returns> Gets products list query. </returns>
    public static GetProductsQuery GetProductsQuery()
    {
        return new GetProductsQuery();
    }
}