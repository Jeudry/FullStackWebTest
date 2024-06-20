using Application.Common.Interfaces;
using Domain.User;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Users;

internal sealed class UserRepository(AppDbContext appDbContext): IUserRepository
{
    /// <summary>
    /// Saves the changes asynchronously.
    /// </summary>
    /// <param name="cancellationToken"> The cancellation token. </param>
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    => await appDbContext.SaveChangesAsync(cancellationToken);

    /// <summary>
    /// see cref="IUserRepository.IsUserUniqueAsync(string, CancellationToken)"/>
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="none"></param>
    /// <returns> If the email is unique. </returns>
    public async Task<bool> IsUsernameUniqueAsync(string userName, CancellationToken none) => 
        !await appDbContext.Users.AnyAsync(user => user.UserName == userName, none);

    /// <summary>
    /// see cref="IUserRepository.IsEmailUniqueAsync(string, CancellationToken)"/>
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken) => 
        !await appDbContext.Users.AnyAsync(user => user.Email == email, cancellationToken);

    /// <summary>
    /// see cref="IUserRepository.GetByUserNameAsync(string)"/>
    /// </summary>
    /// <param name="requestUserName"></param>
    /// <returns></returns>
    public Task<User?> GetByUserNameAsync(string requestUserName)
    => appDbContext.Users.FirstOrDefaultAsync(user => user.UserName == requestUserName);
    
    /// <summary>
    /// Get user by id.
    /// </summary>
    /// <param name="id"> The user id. </param>
    /// <returns></returns>
    public Task<User?> GetByIdAsync(string id) => appDbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
}