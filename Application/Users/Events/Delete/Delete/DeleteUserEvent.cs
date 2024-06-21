using ErrorOr;
using MediatR;

namespace Application.Users.Events.Delete.Delete;

/// <summary>
/// Command to delete a product.
/// </summary>
/// <param name="UserId">Identifier of the user to delete</param>
public record DeleteUserEvent(Guid UserId) : IRequest<ErrorOr<Success>>;