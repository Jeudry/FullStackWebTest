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
    /// Product should return error when product code already exists
    /// </summary>
    [Fact]
    public async Task UpdateProductCommand_Should_ReturnError_WhenExisting_ProductCode()
    {
        var createProductCommand = ProductsCommandFactory.CreateProductCommand();
        
        await _mediator.Send(createProductCommand); 
        
        var otherProduct = ProductsCommandFactory.CreateProductCommand(
                code: "zxh-00"
            );
        
        await _mediator.Send(otherProduct);
        
        var productUpdateCommand = ProductsCommandFactory.UpdateProductCommand(
            "Update Product",
            otherProduct.Code,
            createProductCommand.Description,
            createProductCommand.Price,
            createProductCommand.Stock,
            createProductCommand.Id
            );
        
        var result = await _mediator.Send(productUpdateCommand);

        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
    }
}