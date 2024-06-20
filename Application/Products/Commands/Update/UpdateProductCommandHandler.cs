using Application.Common.Interfaces;
using Application.Products.Commands.Create;
using Domain.Product;
using ErrorOr;
using MediatR;
using Triplex.Validations;

namespace Application.Products.Commands.Update;

internal sealed class UpdateProductCommandHandler(IProductRepository productRepository):  IRequestHandler<UpdateProductCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(UpdateProductCommand request, CancellationToken cancellationToken = default)
    {
        Arguments.NotNull(request, nameof(request));
        
        Product? existingProduct = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (existingProduct is null)
            return Error.NotFound(description: "Product not found");
        
        existingProduct.Update(request.Name, request.Code, request.Price, request.Stock, request.CreatedAt, request.Description, request.UpdatedAt);
        
         productRepository.UpdateAsync(existingProduct);

        return new Success();
    }
}
