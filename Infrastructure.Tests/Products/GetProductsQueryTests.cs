using Application.Products.Responses;
using FluentAssertions;
using Infrastructure.Tests.Common;
using MediatR;
using TestCommon.Products;

namespace Infrastructure.Tests.Products;

/// <summary>
/// Test class for GetProductsQuery
/// </summary>
/// <param name="webAppFactory"> WebAppFactory instance </param>
[Collection(WebAppFactoryCollection.CollectionName)]
public class GetProductsQueryTests(WebAppFactory webAppFactory)
{
    private readonly IMediator _mediator = webAppFactory.CreateMediator();

    [Fact]
    public async Task GetProductsQuery_Should_Return_Products()
    {
        var createProductCommand = ProductsCommandFactory.CreateProductCommand(id: Guid.NewGuid());
        await _mediator.Send(createProductCommand);

        var getProductsCommand = ProductsCommandFactory.GetProductsQuery();
        var getProductsResult = await _mediator.Send(getProductsCommand);

        getProductsResult.IsError.Should().BeFalse();
        getProductsResult.Value.Items.Should().NotBeEmpty();
        getProductsResult.Value.Items.Should().AllBeOfType<ProductResponse>();
    }
}