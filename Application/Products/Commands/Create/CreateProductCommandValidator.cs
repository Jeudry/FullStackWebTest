using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Products.Commands.Create;

/// <summary>
/// Validator for the <see cref="CreateProductCommand"/>.
/// </summary>
public sealed class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator(IProductRepository productRepository)
    {
        RuleFor(x => x.Code).MustAsync(
            async (code, _) => !await productRepository.IsProductUniqueAsync(code, CancellationToken.None)).WithMessage("The code must be unique.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("The name cant be empty");
        RuleFor(x => x.Name).NotNull().WithMessage("The name cant be null");
        RuleFor(x => x.Code).NotEmpty().WithMessage("The code cant be empty");
        RuleFor(x => x.Code).NotNull().WithMessage("The code cant be null");
        RuleFor(x => x.Description).NotEmpty().WithMessage("The description cant be empty");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("The price must be greater than 0");
        RuleFor(x => x.Price).NotNull().WithMessage("The price cant be null");
        RuleFor(x => x.Stock).NotNull().WithMessage("The stock cant be null");
        RuleFor(x => x.Stock).GreaterThan(0).WithMessage("The stock must be greater than 0");
    }
}