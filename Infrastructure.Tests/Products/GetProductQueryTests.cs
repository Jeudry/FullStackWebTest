using Application.Products.Responses;
using ErrorOr;
using FluentAssertions;
using Infrastructure.Tests.Common;
using MediatR;
using TestCommon.Products;

namespace Infrastructure.Tests.Products;

/// <summary>
/// Test class for GetProductQuery
/// </summary>
/// <param name="webAppFactory"> WebAppFactory instance </param>
[Collection(WebAppFactoryCollection.CollectionName)]
public class GetProductQueryTests(WebAppFactory webAppFactory)
{
    private readonly IMediator _mediator = webAppFactory.CreateMediator();

    /// <summary>
    /// Should return product
    /// </summary>
    [Fact]
    public async Task GetProductQuery_Should_Return_Product()
    {
        var createProductCommand = ProductsCommandFactory.CreateProductCommand(id: Guid.NewGuid());
        await _mediator.Send(createProductCommand);

        var getProductCommand = ProductsCommandFactory.GetProductQuery(createProductCommand.Id);
        var getProductResult = await _mediator.Send(getProductCommand);

        getProductResult.IsError.Should().BeFalse();
        getProductResult.Value.Id.Should().Be(createProductCommand.Id!.Value);
        getProductResult.Value.Should().BeOfType<ProductResponse>();
    }
    
    /// <summary>
    /// Should return error when product not found
    /// </summary>
    [Fact]
    public async Task GetProductQuery_Should_Return_Error_When_Product_Not_Found()
    {
        var getProductCommand = ProductsCommandFactory.GetProductQuery(Guid.NewGuid());
        var getProductResult = await _mediator.Send(getProductCommand);

        getProductResult.IsError.Should().BeTrue();
        getProductResult.FirstError.Type.Should().Be(ErrorType.NotFound);
    }
}