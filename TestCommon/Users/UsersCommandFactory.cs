using Application.Users.Commands.Register;
using TestCommon.TestsConstants;

namespace TestCommon.Users;

/// <summary>
/// Factory for creating users commands.
/// </summary>
public sealed class UsersCommandFactory
{
    
    public static RegisterUserCommand RegisterUserCommand(
        string userName = Constants.User.UserName,
        string email = Constants.User.Email,
        string password = Constants.User.Password,
        string confirmPassword = Constants.User.ConfirmPassword,
        Guid? id = null
    )
    {
        return new RegisterUserCommand
        (
            userName,
            email,
            password,
            confirmPassword
        );
    }
}