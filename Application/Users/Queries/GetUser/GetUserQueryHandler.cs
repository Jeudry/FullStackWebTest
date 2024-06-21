using Application.Common.Interfaces;
using Application.Users.response;
using Domain.User;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Triplex.Validations;

namespace Application.Users.Queries.GetUser;

internal sealed class GetUserQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserQuery, ErrorOr<UserResponse>>
{
    public async Task<ErrorOr<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken = default)
    {
        Arguments.NotNull(request, nameof(request));
        
        User? user = await userRepository.GetByIdAsync(request.Id.ToString(), cancellationToken);
        
        if (user is null)
        {
            return Error.NotFound();
        }
        
        List<IdentityRole> roles = await userRepository.GetUserRolesAsync(user);

        UserResponse response = new UserResponse(
            user.Id,
            user.UserName!,
            user.Email!,
            roles.Select(role => new RoleResponse(role.Id, role.Name!)).ToList()
        );
        
        return response;
    }
}