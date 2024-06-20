using Application.Common.Interfaces;
using Application.Products.Responses;
using Domain.Product;
using ErrorOr;
using MediatR;
using Triplex.Validations;

namespace Application.Products.Queries.GetProduct;

internal sealed class GetProductQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductQuery, ErrorOr<ProductResponse>>
{
    public async Task<ErrorOr<ProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken = default)
    {
        Arguments.NotNull(request, nameof(request));
        
        Product? product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (product is null)
        {
            return Error.NotFound();
        }

        ProductResponse response = new ProductResponse(
            product.Id,
            product.Name,
            product.Code,
            product.Description,
            product.Price,
            product.Stock,
            product.CreatedAt,
            product.UpdatedAt
        );
        
        return response;
    }
}