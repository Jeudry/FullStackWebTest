using Application.Products.Commands.Create;
using Application.Products.Commands.Delete;
using Domain.Product;
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
    
    [HttpGet]
    public async Task<ActionResult<Product>> GetProduct(Guid productId)
    {
        return Ok();
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
        
        CreateProductCommand command = new CreateProductCommand(product.Name, product.Code, product.Description, product.Price, product.Stock);
        
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
        
        DeleteProductCommand command = new DeleteProductCommand(productId);
        
        await sender.Send(command);
        
        return Ok();
    }

}