using Application.Common.Interfaces;
using Application.Products.Commands.Create;
using Domain.User;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Triplex.Validations;

namespace Application.Users.Commands.Register;

internal sealed class RegisterUserCommandHandler(IUserRepository userRepository, UserManager<User> userManager):  IRequestHandler<RegisterUserCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        Arguments.NotNull(request, nameof(request));
        
        User user = new User
        {
            Id = request.Id?.ToString() ?? Guid.NewGuid().ToString(),
            UserName = request.UserName,
            Email = request.Email,
            EmailConfirmed = true,
            LockoutEnabled = true,
            LockoutEnd = null,
            TwoFactorEnabled = false,
            AccessFailedCount = 0
        };
        
        await userManager.CreateAsync(user, request.Password);
        
        await userManager.AddToRoleAsync(user, User.DefaultRole);
        
        await userRepository.SaveChangesAsync(cancellationToken);

        return new Success();
    }
}