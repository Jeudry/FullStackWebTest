using Application.Common.Interfaces;
using Application.Users.Commands.Register;
using Domain.User;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Triplex.Validations;

namespace Application.Users.Commands.Create;

internal sealed class CreateUserCommandHandler(IUserRepository userRepository, UserManager<User> userManager, RoleManager<IdentityRole> roleManager):  IRequestHandler<CreateUserCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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
        
        var role = roleManager.Roles.FirstOrDefault(r => r.Id == request.RolesId[0]);
        
        if (role is null)
        {
            return Error.NotFound(description: "Role not found");
        }

        await userManager.AddToRoleAsync(user, role.Name!);
        
        await userRepository.SaveChangesAsync(cancellationToken);

        return new Success();
    }
}