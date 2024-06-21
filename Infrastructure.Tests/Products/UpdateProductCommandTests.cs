using ErrorOr;
using FluentAssertions;
using Infrastructure.Tests.Common;
using MediatR;
using TestCommon.Products;

namespace Infrastructure.Tests.Products;

/// <summary>
/// Test class for UpdateProductCommand
/// </summary>
/// <param name="webAppFactory"></param>
[Collection(WebAppFactoryCollection.CollectionName)]
public class UpdateProductCommandTest(WebAppFactory webAppFactory)
{
    private readonly IMediator _mediator = webAppFactory.CreateMediator();
    
    /// <summary>
    /// Product should return error when product name already exists
    /// </summary>
    [Fact]
    public async Task UpdateProductCommand_Should_ReturnError_WhenExisting_ProductName()
    {
        
        var product = ProductsFactory.CreateProduct();

        var createProductCommand = ProductsCommandFactory.CreateProductCommand(
            name: product.Name,
            id:Guid.NewGuid());
        
        
        await _mediator.Send(createProductCommand); 
        
        var otherProduct = ProductsCommandFactory.CreateProductCommand(
                id: Guid.NewGuid()
            );
        
        await _mediator.Send(otherProduct);
        
        var productUpdateCommand = ProductsCommandFactory.UpdateProductCommand(
            product.Name,
            createProductCommand.Price,
            createProductCommand.Stock,
            DateTime.Now,
            DateTime.Now,
            createProductCommand.Description,
            Guid.NewGuid()
            );
        
        var result = await _mediator.Send(productUpdateCommand);

        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
    }
    
    /// <summary>
    /// Should update product
    /// </summary>
    [Fact]
    public async Task UpdateProductCommand_Should_Update_Product()
    {
        var createProductCommand = ProductsCommandFactory.CreateProductCommand(id:Guid.NewGuid());
        
        await _mediator.Send(createProductCommand);
        
        var productUpdateCommand = ProductsCommandFactory.UpdateProductCommand(
            "Update Product",
            createProductCommand.Price,
            createProductCommand.Stock,
            DateTime.Now,
            DateTime.Now,
            createProductCommand.Description,
            createProductCommand.Id
            );
        
        var result = await _mediator.Send(productUpdateCommand);

        result.IsError.Should().BeFalse();
    }
}