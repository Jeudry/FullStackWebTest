using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces;
using Application.Users.Commands.Register;
using Application.Users.response;
using Domain.User;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Triplex.Validations;

namespace Application.Users.Queries.Login;

internal sealed class LoginUserQueryHandler(IUserRepository userRepository, UserManager<User> userManager, IConfiguration configuration
    )
    : IRequestHandler<LoginUserQuery, ErrorOr<LoginResponse>>
{
    public async Task<ErrorOr<LoginResponse>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        Arguments.NotNull(request, nameof(request));
        
        User? user = await userRepository.GetByUserAsync(request.UserName);
        
        if (user == null) return Error.NotFound("User not found.");
        
        bool result = await userManager.CheckPasswordAsync(user, request.Password);
        
        if(!result) return Error.Unauthorized("Invalid password.");
        
        TokenResponse accessToken = GenerateAccessToken(user);
        
        return new LoginResponse(true, accessToken.Expiration, null, accessToken.Token, "Login successful.");
    }
    
    private TokenResponse GenerateAccessToken(User user)
    {
        var userRole = userManager.GetRolesAsync(user).Result;
        var tokenHandler = new JwtSecurityTokenHandler();
        string secret = configuration["JWT:Secret"] ?? throw new InvalidOperationException();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
             secret));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new (ClaimTypes.NameIdentifier, user.Id),
                new (ClaimTypes.Name, user.UserName ?? string.Empty),
                new (ClaimTypes.Role, userRole.FirstOrDefault() ?? string.Empty),
                new (JwtRegisteredClaimNames.Iss, configuration["JWT:ValidIssuer"] ?? throw new InvalidOperationException()),
                new (JwtRegisteredClaimNames.Aud, configuration["JWT:ValidAudience"] ?? throw new InvalidOperationException()),
            }),
            Issuer = configuration["Issuer"],
            Audience = configuration["Audience"],
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        };

        SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
        TokenResponse tokenResponse = new TokenResponse(
            tokenHandler.WriteToken(token),
            token.ValidTo.ToString(CultureInfo.InvariantCulture)
        );

        return tokenResponse;
    }

}
