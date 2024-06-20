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
        
        Product product = new Product(request.Id ?? Guid.NewGuid(), request.Name, request.Code, request.Description, request.Price, request.Stock);
        
        await productRepository.UpdateAsync(product, cancellationToken);

        return new Success();
    }
}
