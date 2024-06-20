using Application.Common.Interfaces;
using Domain.Product;
using ErrorOr;
using MediatR;
using Triplex.Validations;

namespace Application.Products.Queries.GetProduct;

internal sealed class GetProductQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductQuery, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken = default)
    {
        Arguments.NotNull(request, nameof(request));
        
        Product? product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (product is null)
        {
            return Error.NotFound();
        }
        
        return product;
    }
}