using Application.Common.Interfaces;
using Application.Users.Commands.Register;
using Domain.User;
using FluentValidation;

namespace Application.Users.Queries.Login;

/// <summary>
/// Validator for the LoginUserQuery.
/// </summary>
public sealed class LoginUserQueryValidator:AbstractValidator<RegisterUserCommand>
{

    /// <summary>
    /// Constructor for the LoginUserQueryValidator.
    /// </summary>
    public LoginUserQueryValidator()
    {
        RuleFor(user => user.UserName)
            .MaximumLength(User.MaxUsernameLength).WithMessage($"The username must be at most {User.MaxUsernameLength} characters long.")
            .MinimumLength(User.MinUsernameLength).WithMessage($"The username must be at least {User.MinUsernameLength} characters long.")
            .NotEmpty().WithMessage("The username cant be empty")
            .NotNull().WithMessage("The username cant be null");
        
        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("The password cant be empty")
            .NotNull().WithMessage("The password cant be null")
            .MaximumLength(User.MaxPasswordLength)
            .WithMessage($"The password must be at most {User.MaxEmailLength} characters long")
            .MinimumLength(User.MinPasswordLength)
            .WithMessage($"The password must be at least {User.MinPasswordLength} characters long");
    }
}