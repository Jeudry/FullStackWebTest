using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Products.Commands.Create;

public sealed class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator(IProductRepository productRepository)
    {
        RuleFor(x => x.Code).MustAsync(
            async (code, _) => !await productRepository.IsProductUniqueAsync(code, CancellationToken.None)).WithMessage("The code must be unique.");
    }
}