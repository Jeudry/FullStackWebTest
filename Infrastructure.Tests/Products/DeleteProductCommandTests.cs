using ErrorOr;
using FluentAssertions;
using Infrastructure.Tests.Common;
using MediatR;
using TestCommon.Products;

namespace Infrastructure.Tests.Products;

/// <summary>
/// Test class for DeleteProductCommand
/// </summary>
/// <param name="webAppFactory">WebAppFactory instance</param>
[Collection(WebAppFactoryCollection.CollectionName)]
public class DeleteProductCommandTests(WebAppFactory webAppFactory)
{
    private readonly IMediator _mediator = webAppFactory.CreateMediator();

    
    /// <summary>
    /// Should return error when product not found
    /// </summary>
    [Fact]
    public async Task DeleteProductCommand_Should_ReturnError_When_ProductNotFound()
    {
        var createProductCommand = ProductsCommandFactory.CreateProductCommand();
        
        await _mediator.Send(createProductCommand);
        
        var deleteProductCommand = ProductsCommandFactory.DeleteProductCommand(Guid.NewGuid());
        
        var result = await _mediator.Send(deleteProductCommand);

        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.NotFound);
    }
    
    /// <summary>
    /// Should delete product
    /// </summary>
    [Fact]
    public async Task DeleteProductCommand_Should_Delete_Product()
    {
        var createProductCommand = ProductsCommandFactory.CreateProductCommand(id:Guid.NewGuid());
        
        await _mediator.Send(createProductCommand);
        
        var deleteProductCommand = ProductsCommandFactory.DeleteProductCommand(createProductCommand.Id);
        
        var result = await _mediator.Send(deleteProductCommand);

        result.IsError.Should().BeFalse();
    }
}