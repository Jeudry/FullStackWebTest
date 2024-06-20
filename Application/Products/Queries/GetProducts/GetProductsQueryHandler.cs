using System.Collections;
using Application.Common;
using Application.Common.Interfaces;
using Application.Products.Queries.GetProduct;
using Application.Products.Responses;
using Application.Users.response;
using Domain.Product;
using ErrorOr;
using MediatR;
using Triplex.Validations;

namespace Application.Products.Queries.GetProducts;

internal sealed class GetProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductsQuery, ErrorOr<ListResponse<ProductResponse>>>
{
    public async Task<ErrorOr<ListResponse<ProductResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        Arguments.NotNull(request, nameof(request));
        
        List<Product> products = await productRepository.GetAllAsync(request.SortBy, request.Direction, request.Limit, request.Offset, request.Search, cancellationToken);
        
        int total = await productRepository.GetTotalCountAsync(cancellationToken);
        
        List<ProductResponse> response = products.Select(product => new ProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Stock,
            product.CreatedAt,
            product.UpdatedAt
        )).ToList();
        
        ListResponse<ProductResponse> responseList = new ListResponse<ProductResponse>(response, total);
        
        return responseList;
    }
}