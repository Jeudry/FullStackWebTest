using Application.Common;
using Application.Common.Interfaces;
using Domain.Product;
using ErrorOr;
using MediatR;
using Result = Application.Shared.Result;

namespace Application.Products.Commands.Create;

internal sealed class CreateProductCommandHandler(IProductRepository productRepository):  IRequestHandler<CreateProductCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = new Product(Guid.NewGuid(), request.Name, request.Code, request.Description, request.Price, request.Stock);
        
        await productRepository.AddAsync(product, cancellationToken);

        return new Success();
    }
}