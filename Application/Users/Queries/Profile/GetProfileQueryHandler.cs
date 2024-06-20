using Application.Common.Interfaces;
using Application.Users.response;
using Domain.User;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Configuration;
using Triplex.Validations;

namespace Application.Users.Queries.Profile;

internal sealed class GetProfileQueryHandler(IUserRepository userRepository)
    : IRequestHandler<GetProfileQuery, ErrorOr<UserResponse>>
{
    public async Task<ErrorOr<UserResponse>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        Arguments.NotNull(request, nameof(request));
        
        User? user = await userRepository.GetByIdAsync(request.UserId.ToString());
        
        if (user == null) return Error.NotFound("User not found.");
        
        return new UserResponse(user.UserName!, user.Email!);
    }
}
