using Application.Users.response;
using ErrorOr;
using FluentAssertions;
using Infrastructure.Tests.Common;
using MediatR;
using TestCommon.Users;

namespace Infrastructure.Tests.Users;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetProfileQueryTests(WebAppFactory webAppFactory)
{
    private readonly IMediator _mediator = webAppFactory.CreateMediator();
    
    [Fact]
    public async Task GetProfileQuery_Should_Return_NotFound_Error_When_User_Not_Found()
    {
        var getProfileQuery = UsersCommandFactory.GetGetProfileQuery();
        
        var result = await _mediator.Send(getProfileQuery);
        
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.NotFound);
    }
    
    [Fact]
    public async Task GetProfileQuery_Should_Return_Profile()
    {
        var user = UsersFactory.CreateUser();
        var registerUserCommand = UsersCommandFactory.RegisterUserCommand(
            user.UserName!,
            user.Email!,
            id: Guid.NewGuid()
        );

        await _mediator.Send(registerUserCommand);

        var getProfileQuery = UsersCommandFactory.GetGetProfileQuery(
            registerUserCommand.Id.ToString()
            );

        var result = await _mediator.Send(getProfileQuery);

        result.IsError.Should().BeFalse();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<UserResponse>();
    }
}