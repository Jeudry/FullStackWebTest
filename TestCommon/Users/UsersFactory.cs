using Domain.User;
using TestCommon.TestsConstants;

namespace TestCommon.Users;

/// <summary>
/// Factory for creating users.
/// </summary>
public static class UsersFactory
{
    /// <summary>
    /// Create a user.
    /// </summary>
    /// <param name="id"> Identifier of the user. </param>
    /// <param name="userName"> Name of the user. </param>
    /// <param name="email"> Email of the user. </param>
    /// <param name="password"> Password of the user. </param>
    /// <param name="confirmPassword"> Confirm password of the user. </param>
    /// <returns></returns>
    public static User CreateUser(
        Guid? id = null, 
        string userName = Constants.User.UserName,
        string email = Constants.User.Email,
        string password = Constants.User.Password,
        string confirmPassword = Constants.User.ConfirmPassword
    )
    {
        return new User
        {
            Id = id.ToString() ?? Constants.User.Id.ToString(),
            UserName = userName,
            Email = email
        };
    }
            
}