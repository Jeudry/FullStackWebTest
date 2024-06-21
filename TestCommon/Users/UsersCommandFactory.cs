using Application.Users.Commands.Register;
using Application.Users.Queries.Login;
using Application.Users.Queries.Profile;
using Application.Users.response;
using Domain.User;
using MediatR;
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
            confirmPassword,
            id ?? Constants.User.Id
        );
    }

    /// <summary>
    /// Login user query
    /// </summary>
    /// <param name="userEmail"> User email</param>
    /// <param name="password"> Invalid password</param>
    /// <returns> IRequest</returns>
    public static LoginUserQuery GetLoginUserQuery(
        string userEmail = Constants.User.Email,
        string password = Constants.User.Password
        )
    {
        return new LoginUserQuery(userEmail, password);
    }

    /// <summary>
    /// Get profile query
    /// </summary>
    /// <returns>Get Profile Query instance</returns>
    public static GetProfileQuery GetGetProfileQuery(string? userId = null)
    {
        return new GetProfileQuery(userId ?? Guid.NewGuid().ToString());
    }
}