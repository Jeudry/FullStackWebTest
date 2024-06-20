using System.Net;
using System.Security.Claims;
using Application.Users.Commands.Register;
using Application.Users.Queries.Login;
using Application.Users.Queries.Profile;
using Application.Users.response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Users;
using Triplex.Validations;

namespace FullStackDevTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsersController(
    ISender sender
    ): ControllerBase
{
    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="userRequest"> user request </param>
    /// <returns> returns action result </returns>
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] RegisterUserRequest userRequest)
    {
        Arguments.NotNull(userRequest, nameof(userRequest));
        
        var registerUserCommand = new RegisterUserCommand(
            userRequest.UserName,
            userRequest.Email,
            userRequest.Password,
            userRequest.ConfirmPassword,
            userRequest.Id
        );
        
        var result = await sender.Send(registerUserCommand);
        if (result.IsError)
        {
            return BadRequest();
        }
        return Ok(result);
    }
    
    /// <summary>
    /// Login user
    /// </summary>
    /// <param name="userRequest"> user request </param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> LoginUser([FromBody] LoginUserRequest userRequest)
    {
        Arguments.NotNull(userRequest, nameof(userRequest));
        
        var loginUserCommand = new LoginUserQuery(
            userRequest.UserName,
            userRequest.Password
        );
        
        var result = await sender.Send(loginUserCommand);
        if (result.IsError)
        {
            return BadRequest();
        }
        return Ok(result);
    }
    
    /// <summary>
    /// Get current profile
    /// </summary>
    /// <returns></returns>
    [HttpGet("get-current-profile")]
    [Authorize]
    public async Task<IActionResult> GetCurrentProfile()
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return StatusCode((int)HttpStatusCode.Unauthorized);
        
        var getUserQuery = new GetProfileQuery(userId);
        
        var result = await sender.Send(getUserQuery);
        if (result.IsError)
        {
            return NotFound();
        }
        return Ok(result);
    }
}