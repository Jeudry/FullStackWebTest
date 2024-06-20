using ErrorOr;
using FluentAssertions;
using Infrastructure.Tests.Common;
using MediatR;
using TestCommon.Users;

namespace Infrastructure.Tests.Users;

/// <summary>
/// Test class for RegisterUserCommand
/// </summary>
/// <param name="webAppFactory"> WebAppFactory instance</param>
[Collection(WebAppFactoryCollection.CollectionName)]
public class RegisterUserCommandTests(WebAppFactory webAppFactory)
{
    private readonly IMediator _mediator = webAppFactory.CreateMediator();

    /// <summary>
    /// Register user should return error when existing email
    /// </summary>
    [Fact]
    public async Task RegisterUserCommand_Should_Return_ValidateError_When_Existing_Email()
    {
        var user = UsersFactory.CreateUser();
        var registerUserCommand = UsersCommandFactory.RegisterUserCommand(
            user.UserName!,
            user.Email!
        );

        await _mediator.Send(registerUserCommand);
        
        var otherUser = UsersFactory.CreateUser(id: Guid.NewGuid(), userName: "otherUsername");
        var otherRegisterUserCommand = UsersCommandFactory.RegisterUserCommand(
            otherUser.UserName!,
            user.Email!,
            id: Guid.Parse(otherUser.Id)
        );

        var result = await _mediator.Send(otherRegisterUserCommand);

        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
    }
    
    [Fact]
    public async Task RegisterUserCommand_Should_Create_User()
    {
        var user = UsersFactory.CreateUser();
        var registerUserCommand = UsersCommandFactory.RegisterUserCommand(
            user.UserName!,
            user.Email!
        );

        var result = await _mediator.Send(registerUserCommand);

        result.IsError.Should().BeFalse();
    }
    
    [Fact]
    public async Task RegisterUserCommand_Should_Return_ValidationError_When_Passwords_Do_Not_Match()
    {
        var user = UsersFactory.CreateUser();
        var registerUserCommand = UsersCommandFactory.RegisterUserCommand(
            user.UserName!,
            user.Email!,
            "password",
            "wrongPassword"
        );

        var result = await _mediator.Send(registerUserCommand);

        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
    }
    
    [Fact]
    public async Task RegisterUserCommand_Should_Return_ValidationError_When_Password_Is_Too_Short()
    {
        var user = UsersFactory.CreateUser();
        var registerUserCommand = UsersCommandFactory.RegisterUserCommand(
            user.UserName!,
            user.Email!,
            "pass",
            "pass"
        );

        var result = await _mediator.Send(registerUserCommand);

        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
    }
    
    [Fact]
    public async Task RegisterUserCommand_Should_Return_ValidationError_When_Existing_Username()
    {
        var user = UsersFactory.CreateUser();
        var registerUserCommand = UsersCommandFactory.RegisterUserCommand(
            user.UserName!,
            user.Email!
        );

        await _mediator.Send(registerUserCommand);
        
        var otherUser = UsersFactory.CreateUser(id: Guid.NewGuid(), email: "otherEmail@exmaple.com");
        var otherRegisterUserCommand = UsersCommandFactory.RegisterUserCommand(
            user.UserName!,
            otherUser.Email!,
            id: Guid.Parse(otherUser.Id)
        );
        
        var result = await _mediator.Send(otherRegisterUserCommand);

        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
    }
}