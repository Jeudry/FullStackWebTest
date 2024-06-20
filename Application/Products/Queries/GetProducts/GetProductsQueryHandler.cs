using System.Collections;
using Application.Common.Interfaces;
using Application.Products.Queries.GetProduct;
using Domain.Product;
using ErrorOr;
using MediatR;
using Triplex.Validations;

namespace Application.Products.Queries.GetProducts;

internal sealed class GetProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductsQuery, ErrorOr<List<Product>>>
{
    public async Task<ErrorOr<List<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        Arguments.NotNull(request, nameof(request));
        
        List<Product> products = await productRepository.GetAllAsync(cancellationToken);
        
        return products;
    }
}