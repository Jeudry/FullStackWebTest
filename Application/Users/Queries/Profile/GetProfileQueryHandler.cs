using Application.Common.Interfaces;
using Application.Users.response;
using Domain.User;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Triplex.Validations;

namespace Application.Users.Queries.Profile;

internal sealed class GetProfileQueryHandler(IUserRepository userRepository)
    : IRequestHandler<GetProfileQuery, ErrorOr<UserResponse>>
{
    public async Task<ErrorOr<UserResponse>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        Arguments.NotNull(request, nameof(request));
        
        User? user = await userRepository.GetByIdAsync(request.UserId);
        
        if (user == null) return Error.NotFound("User not found.");
        
        List<IdentityRole> roles = await userRepository.GetUserRolesAsync(user);
        
        return new UserResponse(id: user.Id, user.UserName!, user.Email!, roles.Select(role => new RoleResponse(role.Id, role.Name!)).ToList());
    }
}
