using Application.Common.Interfaces;
using Domain.Product;
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
            async (code, _) => !await productRepository.IsProductUniqueAsync(code, CancellationToken.None)).WithMessage("The code must be unique.")
            .NotEmpty().WithMessage("The code cant be empty")
            .NotNull().WithMessage("The code cant be null");
            
        RuleFor(x => x.Name).NotEmpty().WithMessage("The name cant be empty")
            .NotNull().WithMessage("The name cant be null")
            .MinimumLength(Product.NameMinLength).WithMessage($"The name must have at least {Product.NameMinLength} characters")
            .MaximumLength(Product.NameMaxLength).WithMessage($"The name must have at most {Product.NameMaxLength} characters");
        
        RuleFor(x => x.Description).NotEmpty().WithMessage("The description cant be empty")
            .MinimumLength(Product.DescriptionMinLength).WithMessage($"The description must have at least {Product.DescriptionMinLength} characters")
            . MaximumLength(Product.DescriptionMaxLength).WithMessage($"The description must have at most {Product.DescriptionMaxLength} characters");
        
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("The price must be greater than 0")
            .NotNull().WithMessage("The price cant be null");
        
        RuleFor(x => x.Stock).NotNull().WithMessage("The stock cant be null").GreaterThan(0).WithMessage("The stock must be greater than 0");
    }
}