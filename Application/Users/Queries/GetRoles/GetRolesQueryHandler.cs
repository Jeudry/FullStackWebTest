using Application.Common.Interfaces;
using Application.Users.response;
using Domain.User;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Triplex.Validations;

namespace Application.Users.Queries.GetRoles;

internal sealed class GetRolesQueryHandler(IUserRepository userRepository, RoleManager<IdentityRole> roleManager): IRequestHandler<GetRolesQuery, ErrorOr<List<RoleResponse>>>
{
    public async Task<ErrorOr<List<RoleResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
         Arguments.NotNull(request, nameof(request));

         List<IdentityRole> roles = await userRepository.GetRolesAsync(); 
         
         var roleResponses = roles.Select(role => new RoleResponse(role.Id, role.Name!)).ToList();

         return roleResponses;
    }
}