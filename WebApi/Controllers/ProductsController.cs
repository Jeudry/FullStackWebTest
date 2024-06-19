using Application.Products.Commands.Create;
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
    
    [HttpGet]
    public async Task<ActionResult<Product>> GetProduct(Guid productId)
    {
        return Ok();
    }   
}