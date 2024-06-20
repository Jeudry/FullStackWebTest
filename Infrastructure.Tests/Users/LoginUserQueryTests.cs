using Application.Users.Queries.Profile;
using ErrorOr;
using FluentAssertions;
using Infrastructure.Tests.Common;
using MediatR;
using TestCommon.Users;

namespace Infrastructure.Tests.Users;

/// <summary>
/// Test class for LoginUserQuery
/// </summary>
/// <param name="webAppFactory"> WebAppFactory instance</param>
[Collection(WebAppFactoryCollection.CollectionName)]
public class LoginUserQueryTests(WebAppFactory webAppFactory)
{
    private readonly IMediator _mediator = webAppFactory.CreateMediator();

    /// <summary>
    /// Login user should return unauthorized error when invalid credentials
    /// </summary>
    [Fact]
    public async Task LoginUserQuery_Should_Return_UnauthorizedError_When_Invalid_Credentials()
    {
        var user = UsersFactory.CreateUser();
        var registerUserCommand = UsersCommandFactory.RegisterUserCommand(
            user.UserName!,
            user.Email!
        );

        await _mediator.Send(registerUserCommand);

        var loginUserQuery = UsersCommandFactory.GetLoginUserQuery(
            user.UserName!,
            "invalidPassword"
        );

        var result = await _mediator.Send(loginUserQuery);

        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Unauthorized);
    }
    
    /// <summary>
    /// Login user correctly should return login response
    /// </summary>
    [Fact]
    public async Task LoginUserQuery_Should_Return_Login_Response()
    {
        var user = UsersFactory.CreateUser();
        var registerUserCommand = UsersCommandFactory.RegisterUserCommand(
            user.UserName!,
            user.Email!
        );

        await _mediator.Send(registerUserCommand);

        var loginUserQuery = UsersCommandFactory.GetLoginUserQuery(
            user.UserName!,
            registerUserCommand.Password
        );

        var result = await _mediator.Send(loginUserQuery);

        result.IsError.Should().BeFalse();
        result.Value.Should().NotBeNull();
    }
    
    /// <summary>
    /// Login user should return error when user not exist
    /// </summary>
    [Fact]
    public async Task LoginUserQuery_Should_Return_Error_When_User_Not_Exist()
    {
        var loginUserQuery = UsersCommandFactory.GetLoginUserQuery(
            "notExistUser",
            "notExistPassword"
        );

        var result = await _mediator.Send(loginUserQuery);

        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.NotFound);
    }
}