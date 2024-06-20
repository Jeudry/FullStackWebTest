using Domain.User;

namespace Application.Common.Interfaces;

/// <summary>
/// Represents the user repository.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Saves the changes asynchronously.
    /// </summary>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <returns></returns>
    Task SaveChangesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Checks if the user is unique.
    /// </summary>
    /// <param name="userName"> The username. </param>
    /// <param name="none"> The cancellation token. </param>
    /// <returns> If the username is unique. </returns>
    Task<bool> IsUsernameUniqueAsync(string userName, CancellationToken none);

    /// <summary>
    /// Checks if the email is unique.
    /// </summary>
    /// <param name="email"> The user email. </param>
    /// <param name="cancellationToken"> The request cancellation token. </param>
    /// <returns> If the email is unique. </returns>
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the user by username asynchronously.
    /// </summary>
    /// <param name="requestUserName"> The username. </param>
    /// <returns> The user. </returns>
    Task<User?> GetByUserNameAsync(string requestUserName);
    
    /// <summary>
    /// Gets the user by id asynchronously.
    /// </summary>
    /// <param name="id"> The user id. </param>
    /// <returns></returns>
    Task<User?> GetByIdAsync(string id);
}