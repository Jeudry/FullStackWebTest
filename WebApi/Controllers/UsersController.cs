using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Application.Users.Commands.Create;
using Application.Users.Commands.Register;
using Application.Users.Commands.Update;
using Application.Users.Events.Delete.Delete;
using Application.Users.Queries.GetProducts;
using Application.Users.Queries.GetRoles;
using Application.Users.Queries.GetUser;
using Application.Users.Queries.Login;
using Application.Users.Queries.Profile;
using Application.Users.response;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Users;
using Triplex.Validations;

namespace FullStackDevTest.Controllers;

[Route("api/[controller]")]
public sealed class UsersController(
    ISender sender
    ): ApiController 
{
    
    /// <summary>
    /// Get all roles
    /// </summary>
    /// <returns></returns>
    [HttpGet("get-roles")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public async Task<IActionResult> GetRoles()
    {
        var getUsersQuery = new GetRolesQuery();
        
        var result = await sender.Send(getUsersQuery);
        
        return result.Match(
            _ => Ok(result.Value),
            Problem);
    }
    
    /// <summary>
    /// Get user by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var getUserQuery = new GetUserQuery(id);
        
        var result = await sender.Send(getUserQuery);

        return result.Match(
            _ => Ok(result.Value),
            Problem);
    }
    
    [HttpPut("{productId:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid productId, [FromBody] UpdateUserRequest userRequest)
    {
        Arguments.NotNull(userRequest, nameof(userRequest));
        
        var updateUserCommand = new UpdateUserCommand(
            userRequest.UserName,
            userRequest.Email,
            userRequest.RolesId,
            productId
        );
        
        var result = await sender.Send(updateUserCommand);
        return  result.Match(
            _ => Ok(result.Value),
            Problem);
    }
    
    [HttpGet("get-users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public async Task<IActionResult> GetUsers(
        [FromQuery] string sortBy, [FromQuery] string direction, [FromQuery] int limit, [FromQuery] int offset, [FromQuery] string? search = null)
    {
        var getUsersQuery = new GetUsersQuery(sortBy, direction, limit, offset, search);
        
        var result = await sender.Send(getUsersQuery);
        if (result.IsError)
        {
            return BadRequest();
        }
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Get all users
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    [HttpDelete("{productId:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(Guid productId)
    {
        var deleteUserCommand = new DeleteUserEvent(productId);
        
        var result = await sender.Send(deleteUserCommand);
        return result.Match(
            _ => Ok(result.Value),
            Problem);
    }
    
    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="userRequest"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest userRequest)
    {
        Arguments.NotNull(userRequest, nameof(userRequest));
        
        var createUserCommand = new CreateUserCommand(
            userRequest.UserName,
            userRequest.Email,
            userRequest.Password,
            userRequest.ConfirmPassword,
            userRequest.RolesId,
            userRequest.Id
        );
        
        var result = await sender.Send(createUserCommand);
        return result.Match(
            _ => Ok(result.Value),
            Problem);
    }
    
    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="userRequest"> user request </param>
    /// <returns> returns action result </returns>
    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest userRequest)
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
        return result.Match(
            _ => Ok(result.Value),
            Problem);
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
        return result.Match(
            _ => Ok(result.Value),
            Problem);
    }
    
    /// <summary>
    /// Get current profile
    /// </summary>
    /// <returns></returns>
    [HttpGet("get-current-profile")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetCurrentProfile()
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return StatusCode((int)HttpStatusCode.Unauthorized);
        
        var getUserQuery = new GetProfileQuery(userId);
        
        var result = await sender.Send(getUserQuery);
       
        return result.Match(
            _ => Ok(result.Value),
            Problem);
    }
}