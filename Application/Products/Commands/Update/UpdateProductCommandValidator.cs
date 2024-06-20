using Application.Common.Interfaces;
using Domain.Product;
using FluentValidation;

namespace Application.Products.Commands.Update;

/// <summary>
/// Validator for the <see cref="UpdateProductCommand"/>.
/// </summary>
public sealed class UpdateProductCommandValidator: AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator(IProductRepository productRepository)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("The name cant be empty")
            .MustAsync(
                async (name, _) => await productRepository.IsProductNameUniqueAsync(name, CancellationToken.None)).WithMessage("The name must be unique.")
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