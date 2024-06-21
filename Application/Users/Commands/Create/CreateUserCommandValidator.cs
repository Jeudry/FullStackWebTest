using Application.Common.Interfaces;
using Application.Users.Commands.Register;
using Domain.User;
using FluentValidation;

namespace Application.Users.Commands.Create;

/// <summary>
/// Validator for the CreateUserCommand.
/// </summary>
public sealed class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
{
    /// <summary>
    /// Constructor for the CreateUserCommandValidator.
    /// </summary>
    /// <param name="userRepository"></param>
    public CreateUserCommandValidator(IUserRepository userRepository)
    {
        RuleFor(user => user.UserName).MustAsync(
                async (userName, _) => await userRepository.IsUsernameUniqueAsync(userName, CancellationToken.None)).WithMessage("The username must be unique.")
            .MaximumLength(User.MaxUsernameLength).WithMessage($"The username must be at most {User.MaxUsernameLength} characters long.")
            .MinimumLength(User.MinUsernameLength).WithMessage($"The username must be at least {User.MinUsernameLength} characters long.")
            .NotEmpty().WithMessage("The username cant be empty")
            .NotNull().WithMessage("The username cant be null");

        RuleFor(user => user.Email)
            .MustAsync(async (email, _) => await userRepository.IsEmailUniqueAsync(email, CancellationToken.None)).WithMessage("The email must be unique.")
            .MaximumLength(User.MaxEmailLength).WithMessage($"The email must be at most {User.MaxEmailLength} characters long.")
            .MinimumLength(User.MinEmailLength).WithMessage($"The email must be at least {User.MinEmailLength} characters long.")
            .WithMessage("The email must be unique.")
            .NotEmpty().WithMessage("The email cant be empty")
            .NotNull().WithMessage("The email cant be null");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("The password cant be empty")
            .NotNull().WithMessage("The password cant be null")
            .MaximumLength(User.MaxPasswordLength)
            .WithMessage($"The password must be at most {User.MaxEmailLength} characters long")
            .MinimumLength(User.MinPasswordLength)
            .WithMessage($"The password must be at least {User.MinPasswordLength} characters long");
        
        RuleFor(user => user.ConfirmPassword)
            .Equal(user => user.Password).WithMessage("The password and confirm password must be the same.")
            .NotEmpty().WithMessage("The confirm password cant be empty")
            .NotNull().WithMessage("The confirm password cant be null")
            .MaximumLength(User.MaxPasswordLength).WithMessage($"The confirm password must be at most {User.MaxEmailLength} characters long")
            .MinimumLength(User.MinPasswordLength).WithMessage($"The confirm password must be at least {User.MinPasswordLength} characters long");
    }
}