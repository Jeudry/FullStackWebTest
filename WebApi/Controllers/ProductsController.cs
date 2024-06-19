using Application.Products.Create;
using Domain.Products;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FullStackDevTest.Controllers;

public sealed class ProductsController: ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product, ISender sender)
    {
        CreateProductCommand command = new CreateProductCommand(product.Name);


        await sender.Send(command);
        
        return Ok();
    }
    
    [HttpGet]
    public async Task<ActionResult<Product>> GetProduct(Guid productId)
    {
        return Ok();
    }   
}