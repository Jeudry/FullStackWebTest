using Application.Common;
using Application.Products.Commands.Create;
using Application.Products.Commands.Update;
using Application.Products.Events.Delete;
using Application.Products.Queries.GetProduct;
using Application.Products.Queries.GetProducts;
using Application.Products.Responses;
using Domain.Product;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Products;
using Triplex.Validations;

namespace FullStackDevTest.Controllers;

/// <summary>
/// Represents a controller for products.
/// </summary>
/// <param name="sender">MediatR sender</param>
[ApiController]
[Route("api/[controller]")]
public sealed class ProductsController(ISender sender): ControllerBase
{
    /// <summary>
    /// Gets all products.
    /// </summary>
    /// <param name="sortBy">Sort by field</param>
    /// <param name="direction">Sort direction</param>
    /// <param name="limit">Limit of products to return</param>
    /// <param name="offset">Offset of products to return</param>
    /// <param name="search">Search term to filter products</param>
    /// <returns> returns a list of products </returns>
    [HttpGet]
    public async Task<ActionResult<ListResponse<ProductResponse>>> GetProducts([FromQuery] string sortBy, [FromQuery] string direction, [FromQuery] int limit, [FromQuery] int offset, [FromQuery] string? search = null)
    {
        GetProductsQuery query = new GetProductsQuery(sortBy, direction, limit, offset, search);
        
        ErrorOr<ListResponse<ProductResponse>> result = await sender.Send(query);
        
        if (result.IsError)
            return NotFound();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Gets a product by its identifier.
    /// </summary>
    /// <param name="productId">Identifier of the product</param>
    /// <returns></returns>
    [HttpGet("{productId:guid}")]
    public async Task<ActionResult<ProductResponse>> GetProduct(Guid productId)
    {
        Arguments.NotEmpty(productId, nameof(productId));
        
        GetProductQuery query = new GetProductQuery(productId);
        
        ErrorOr<ProductResponse> result = await sender.Send(query);
        
        if (result.IsError)
            return NotFound();
        
        return Ok(result.Value);
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="product">Product to create</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateProduct(AddProductRequest product)
    {
        Arguments.NotNull(product, nameof(product));
        
        CreateProductCommand command = new CreateProductCommand(product.Name, product.Code,  product.Price, product.Stock, null, product.Description);
        
        await sender.Send(command);
        
        return Ok();
    }
    
    /// <summary>
    /// Deletes an existing product.
    /// </summary>
    /// <param name="productId">Identifier of the product</param>
    /// <returns></returns>
    [HttpDelete("{productId:guid}")]
    public async Task<ActionResult> DeleteProduct(Guid productId)
    {
        Arguments.NotEmpty(productId, nameof(productId));
        
        DeleteProductEvent @event = new DeleteProductEvent(productId);
        
        await sender.Send(@event);
        
        return Ok();
    }
    
    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="productId">Identifier of the product</param>
    /// <param name="product"> Updated product</param>
    /// <returns> returns a product </returns>
    [HttpPut("{productId:guid}")]
    public async Task<ActionResult> UpdateProduct(Guid productId, UpdateProductRequest product)
    {
        Arguments.NotEmpty(productId, nameof(productId));
        Arguments.NotNull(product, nameof(product));
        
        UpdateProductCommand command = new UpdateProductCommand(product.Id, product.Name, product.Code, product.Price, product.Stock, product.CreatedAt, product.Description, product.UpdatedAt);
        
        await sender.Send(command);
        
        return Ok();
    }
}