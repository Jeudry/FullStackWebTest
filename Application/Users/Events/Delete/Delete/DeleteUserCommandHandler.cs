using Application.Common.Interfaces;
using Domain.Product;
using Domain.User;
using ErrorOr;
using MediatR;
using Microsoft.AspNet.Identity;
using Triplex.Validations;

namespace Application.Users.Events.Delete.Delete;

internal sealed class DeleteUserEventHandler(IUserRepository userRepository):  IRequestHandler<DeleteUserEvent, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteUserEvent request, CancellationToken cancellationToken)
    {
        Arguments.NotNull(request, nameof(request));
        
        User? user = await userRepository.GetByIdAsync(request.UserId.ToString(), cancellationToken);
        
        if (user is null)
            return Error.NotFound("User not found.");
        
        userRepository.Delete(user);

        return new Success();
    }
}