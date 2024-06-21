using Domain.User;
using Microsoft.AspNetCore.Identity;

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
    /// Checks if the user is unique.
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="id"></param>
    /// <param name="none"></param>
    /// <returns></returns>
    Task<bool> IsUsernameUniqueAsync(string userName, string id, CancellationToken none);
    
    /// <summary>
    /// Checks if the email is unique.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> IsEmailUniqueAsync(string email, string id, CancellationToken cancellationToken);

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
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the user.
    /// </summary>
    /// <param name="user"></param>
    void Delete(User user);

    /// <summary>
    /// Gets the total count of users.
    /// </summary>
    /// <param name="requestSortBy"></param>
    /// <param name="requestDirection"></param>
    /// <param name="requestLimit"></param>
    /// <param name="requestOffset"></param>
    /// <param name="requestSearch"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<User>> GetAllAsync(string requestSortBy, string requestDirection, int requestLimit, int requestOffset, string? requestSearch, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the total count of users.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> GetTotalCountAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Gets the roles asynchronously.
    /// </summary>
    Task<List<IdentityRole>> GetRolesAsync();

    /// <summary>
    /// Gets the user roles asynchronously.
    /// </summary>
    /// <param name = "user"> The user. </param>
    /// <returns></returns>
    Task<List<IdentityRole>> GetUserRolesAsync(User user);
}