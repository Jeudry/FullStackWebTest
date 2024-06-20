using Application.Common.Interfaces;
using Application.Products.Commands.Create;
using Domain.Product;
using ErrorOr;
using MediatR;
using Triplex.Validations;

namespace Application.Products.Commands.Delete;

internal sealed class DeleteProductCommandHandler(IProductRepository productRepository):  IRequestHandler<DeleteProductCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        Arguments.NotNull(request, nameof(request));
        
        Product? product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        
        if (product is null)
            return Error.NotFound("Product not found.");
        
        productRepository.DeleteAsync(product);

        return new Success();
    }
}