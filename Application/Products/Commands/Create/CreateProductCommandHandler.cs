using Application.Common;
using Application.Common.Interfaces;
using Domain.Product;
using ErrorOr;
using MediatR;
using Triplex.Validations;
using Result = Application.Shared.Result;

namespace Application.Products.Commands.Create;

internal sealed class CreateProductCommandHandler(IProductRepository productRepository):  IRequestHandler<CreateProductCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(CreateProductCommand request, CancellationToken cancellationToken = default)
    {
        Arguments.NotNull(request, nameof(request));
        
        Product product = new Product(request.Id ?? Guid.NewGuid(), request.Name, request.Description, request.Price, request.Stock, DateTime.Now);
        
        await productRepository.AddAsync(product, cancellationToken);

        return new Success();
    }
}