using Application.Common.Interfaces;
using Domain.User;
using FluentValidation;

namespace Application.Users.Commands.Update;

/// <summary>
/// Validator for the CreateUserCommand.
/// </summary>
public sealed class UpdateUserCommandValidator:AbstractValidator<UpdateUserCommand>
{
    /// <summary>
    /// Constructor for the CreateUserCommandValidator.
    /// </summary>
    /// <param name="userRepository"></param>
    public UpdateUserCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x)
            .MustAsync(async (user, _) =>
                await userRepository.IsEmailUniqueAsync(user.Email, user.Id.ToString()!, CancellationToken.None))
            .MustAsync(
                async (user, _) => await userRepository.IsUsernameUniqueAsync(user.UserName, user.Id.ToString()!, CancellationToken.None)).WithMessage("The username must be unique.")
            .WithMessage("The email must be unique.");
        
        
        RuleFor(user => user.UserName)
            .MaximumLength(User.MaxUsernameLength).WithMessage($"The username must be at most {User.MaxUsernameLength} characters long.")
            .MinimumLength(User.MinUsernameLength).WithMessage($"The username must be at least {User.MinUsernameLength} characters long.")
            .NotEmpty().WithMessage("The username cant be empty")
            .NotNull().WithMessage("The username cant be null");

        RuleFor(user => user.Email)
            .MaximumLength(User.MaxEmailLength).WithMessage($"The email must be at most {User.MaxEmailLength} characters long.")
            .MinimumLength(User.MinEmailLength).WithMessage($"The email must be at least {User.MinEmailLength} characters long.")
            .WithMessage("The email must be unique.")
            .NotEmpty().WithMessage("The email cant be empty")
            .NotNull().WithMessage("The email cant be null");
    }   
}