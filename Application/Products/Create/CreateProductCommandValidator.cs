using System.ComponentModel.DataAnnotations;
using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Products.Create;

public sealed class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator(IProductRepository productRepository)
    {
        RuleFor(x => x.Name).MustAsync(
            async (name, _) => await productRepository.IsProductUniqueAsync(name, CancellationToken.None)).WithMessage("The email must be unique.");
    }
}