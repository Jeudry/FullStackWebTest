using Application.Products.Create;
using Domain.Product;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Products;

namespace FullStackDevTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProductsController(ISender sender): ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateProduct(AddProductRequest product)
    {
        CreateProductCommand command = new CreateProductCommand(product.Name, product.Code);
        
        await sender.Send(command);
        
        return Ok();
    }
    
    [HttpGet]
    public async Task<ActionResult<Product>> GetProduct(Guid productId)
    {
        return Ok();
    }   
}