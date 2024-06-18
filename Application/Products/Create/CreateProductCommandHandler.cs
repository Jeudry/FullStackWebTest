using Application.Common.Interfaces;
using Domain.Products;
using MediatR;

namespace Application.Products.Create;

internal sealed class CreateProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<CreateProductCommand>
{
    public Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = new Product(Guid.NewGuid(), request.Name);
        
        productRepository.AddAsync(product, cancellationToken);

        return Task.CompletedTask;
    }
}