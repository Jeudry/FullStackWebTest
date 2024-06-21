using Application.Common.Interfaces;
using Domain.User;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Users;

internal sealed class UserRepository(AppDbContext appDbContext, RoleManager<IdentityRole> roleManager, UserManager<User> userManager): IUserRepository
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
    /// see cref="IUserRepository.IsUsernameUniqueAsync(string, string, CancellationToken)"/>
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="id"></param>
    /// <param name="none"></param>
    /// <returns></returns>
    public async Task<bool> IsUsernameUniqueAsync(string userName, string id, CancellationToken none)
    {
        return !await appDbContext.Users.AnyAsync(user => user.UserName == userName && user.Id != id, none);
    }

    /// <summary>
    /// see cref="IUserRepository.IsEmailUniqueAsync(string, string, CancellationToken)"/>
    /// </summary>
    /// <param name="email"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> IsEmailUniqueAsync(string email, string id, CancellationToken cancellationToken)
    {
        return !await appDbContext.Users.AnyAsync(user => user.Email == email && user.Id != id, cancellationToken);
    }

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
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken) => appDbContext.Users.FirstOrDefaultAsync(user => user.Id == id);

    /// <summary>
    /// Deletes the user.
    /// </summary>
    /// <param name="user"></param>
    public void Delete(User user)
    {
        appDbContext.Users.Remove(user);
        appDbContext.SaveChanges();
    }

    public async Task<List<User>> GetAllAsync(string requestSortBy, string requestDirection, int requestLimit, int requestOffset,
        string? requestSearch, CancellationToken cancellationToken)
    {
        IQueryable<User> query = appDbContext.Users;

        if (!string.IsNullOrWhiteSpace(requestSearch))
        {
            query = query.Where(user => user.UserName.Contains(requestSearch));
        }

        if (!string.IsNullOrWhiteSpace(requestSortBy))
        {
            query = requestDirection switch
            {
                "asc" => query.OrderBy(user => user.UserName),
                "desc" => query.OrderByDescending(user => user.UserName),
                _ => query
            };
        }

        return await query.Skip(requestOffset).Take(requestLimit).ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Gets the total count of users.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int> GetTotalCountAsync(CancellationToken cancellationToken)
    => appDbContext.Users.CountAsync(cancellationToken);

    /// <summary>
    /// Gets the roles asynchronously.
    /// </summary>
    public async Task<List<IdentityRole>> GetRolesAsync() {
        return await appDbContext.Roles.ToListAsync();
    }

    public async Task<List<IdentityRole>> GetUserRolesAsync(User user)
    {
        return await appDbContext.Roles
            .Where(role => appDbContext.UserRoles.Any(userRole => userRole.UserId == user.Id && userRole.RoleId == role.Id))
            .ToListAsync();
    }
}