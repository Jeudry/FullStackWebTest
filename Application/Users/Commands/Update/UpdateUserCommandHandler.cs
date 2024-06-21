using Application.Common.Interfaces;
using Domain.User;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Triplex.Validations;

namespace Application.Users.Commands.Update;

internal sealed class UpdateUserCommandHandler(IUserRepository userRepository, UserManager<User> userManager, RoleManager<IdentityRole> roleManager):  IRequestHandler<UpdateUserCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        Arguments.NotNull(request, nameof(request));
        
        User? user = await userRepository.GetByIdAsync(request.Id.ToString()!, cancellationToken);
        
        if (user is null)
            return Error.NotFound("User not found.");
        
        user.UserName = request.UserName;
        user.Email = request.Email;

        await userManager.UpdateAsync(user);
        
        var role = roleManager.Roles.FirstOrDefault(r => r.Id == request.RoleId.ToString());
        
        if (role is null)
        {
            return Error.NotFound(description: "Role not found");
        }

        var roleList = await userManager.GetRolesAsync(user);
        
        await userManager.RemoveFromRolesAsync(user, roleList);
        
        await userManager.AddToRoleAsync(user, role.Name!);
        
        await userRepository.SaveChangesAsync(cancellationToken);

        return new Success();
    }
}