using Application.Products.Commands.Create;
using Application.Shared;
using ErrorOr;
using FluentAssertions;
using Infrastructure.Tests.Common;
using MediatR;
using TestCommon.Products;

namespace Infrastructure.Tests.Products;

/// <summary>
/// Test class for CreateProductCommand
/// </summary>
/// <param name="webAppFactory">WebAppFactory instance</param>
[Collection(WebAppFactoryCollection.CollectionName)]
public class CreateProductCommandTests(WebAppFactory webAppFactory)
{
    private readonly IMediator _mediator = webAppFactory.CreateMediator();
    
    /// <summary>
    /// Product should return error when product name already exists
    /// </summary>
    [Fact]
    public async Task CreateProductCommand_Should_ReturnError_WhenExisting_ProductName()
    {
        var product = ProductsFactory.CreateProduct();
        
        var createProductCommand = ProductsCommandFactory.CreateProductCommand(
            product.Name
            );
        
        await _mediator.Send(createProductCommand); 
        var result = await _mediator.Send(createProductCommand);

        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
    }
    
    /// <summary>
    /// Should create product
    /// </summary>
    [Fact]
    public async Task CreateProductCommand_Should_Create_Product()
    {
        var createProductCommand = ProductsCommandFactory.CreateProductCommand();
        
        var result = await _mediator.Send(createProductCommand);

        result.IsError.Should().BeFalse();
    }
}