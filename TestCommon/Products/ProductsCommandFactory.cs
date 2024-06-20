using Application.Products.Commands.Create;
using Application.Products.Commands.Update;
using Application.Products.Events.Delete;
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
    /// <param name="description"> The description of the product. </param> 
    /// <param name="price"> The price of the product. </param>
    /// <param name="stock"> The stock of the product. </param>
    /// <param name="id"> The id of the product. </param>
    /// <returns> Create product command. </returns>
    public static CreateProductCommand CreateProductCommand(
        string name = Constants.Product.Text,
        string description = Constants.Product.Description,
        double price = Constants.Product.Price,
        int stock = Constants.Product.Stock,
        Guid? id = null
        )
    {
        return new CreateProductCommand
        (
            name,
            price,
            stock,
            id ?? Constants.Product.Id,
            description
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
    public static GetProductsQuery GetProductsQuery(
            string sortBy = Constants.Product.SortBy,
            string direction = Constants.Product.Direction,
            int? limit = null,
            int? offset = null,
            string? search = null
        )
    {
        return new GetProductsQuery(
            sortBy,
            direction,
            limit ?? Constants.Product.Limit,
            offset ?? Constants.Product.Offset,
            search
            );
    }
    
    public static UpdateProductCommand UpdateProductCommand(
        string name = Constants.Product.Text,
        double price = Constants.Product.Price,
        int stock = Constants.Product.Stock,
        DateTime? createdAt = null,
        DateTime? updatedAt = null,
        string? description = null,
        Guid? id = null
    )
    {
        return new UpdateProductCommand
        (
            id ?? Constants.Product.Id,
            name,
            price,
            stock,
            createdAt ?? Constants.Product.CreatedAt,
            description ?? Constants.Product.Description,
            updatedAt
        );
    }
}