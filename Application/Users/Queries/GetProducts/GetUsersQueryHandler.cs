using Application.Common;
using Application.Common.Interfaces;
using Application.Products.Responses;
using Application.Users.response;
using Domain.Product;
using Domain.User;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Triplex.Validations;

namespace Application.Users.Queries.GetProducts;

internal sealed class GetUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUsersQuery, ErrorOr<ListResponse<UserResponse>>>
{
    public async Task<ErrorOr<ListResponse<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        Arguments.NotNull(request, nameof(request));
        
        List<User> users = await userRepository.GetAllAsync(request.SortBy, request.Direction, request.Limit, request.Offset, request.Search, cancellationToken);
        
        int total = await userRepository.GetTotalCountAsync(cancellationToken);
        
        ListResponse<UserResponse> responseList = new ListResponse<UserResponse>([], total);
        
        for(int i = 0; i < users.Count; i++)
        {
            User user = users[i];
            List<IdentityRole> roles = await userRepository.GetUserRolesAsync(user);
            List<RoleResponse> roleResponses = roles.Select(r => new RoleResponse(r.Id, r.Name!)).ToList();
            UserResponse userResponse = new UserResponse(id: users[i].Id, users[i].UserName!, users[i].Email!, roleResponses);
            responseList.Items.Add(userResponse);
        }
        
        return responseList;
    }
}