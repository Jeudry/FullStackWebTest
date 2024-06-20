using System.Collections;
using Application.Common.Interfaces;
using Application.Products.Queries.GetProduct;
using Application.Products.Responses;
using Application.Users.response;
using Domain.Product;
using ErrorOr;
using MediatR;
using Triplex.Validations;

namespace Application.Products.Queries.GetProducts;

internal sealed class GetProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductsQuery, ErrorOr<List<ProductResponse>>>
{
    public async Task<ErrorOr<List<ProductResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        Arguments.NotNull(request, nameof(request));
        
        List<Product> products = await productRepository.GetAllAsync(cancellationToken);
        
        List<ProductResponse> response = products.Select(product => new ProductResponse(
            product.Id,
            product.Name,
            product.Code,
            product.Description,
            product.Price,
            product.Stock,
            product.CreatedAt,
            product.UpdatedAt
        )).ToList();
        
        return response;
    }
}