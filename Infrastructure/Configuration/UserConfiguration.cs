using Domain.Product;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

/// <summary>
/// Represents the user configuration.
/// </summary>
public sealed class UserConfiguration: IEntityTypeConfiguration<User>
{
    private const string AdminId = "dc043262-673a-491a-b811-446703743743";
    private const string UserId = "dc043262-673a-491a-b811-446703743744";
    private const string Admin2Id = "dc043262-673a-491a-b811-446703743745";
    private const string UserId2 = "dc043262-673a-491a-b811-446703743746";
    
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        List<User> users =
        [
            new User
            {
                Id = AdminId,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "18497505944",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnd = null
            },

            new User
            {
                Id = UserId,
                UserName = "User",
                NormalizedUserName = "USER",
                Email = "user@example.com",
                NormalizedEmail = "USER@EXAMPLE.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "18497505945",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnd = null
            },
            new  User
            {
                Id = Admin2Id,
                UserName = "Admin2",
                NormalizedUserName = "ADMIN2",
                Email = "admin2@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "18497505936",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnd = null
            },
            new User
            {
                Id = UserId2,
                UserName = "User2",
                NormalizedUserName = "USER2",
                Email = "user2@example.com",
                NormalizedEmail = "USER@EXAMPLE.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "18497505937",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnd = null
            }
        ];

        foreach (var user in users)
        {
            user.PasswordHash = PassGenerate(user);
            builder.HasData(
                user
            );
        }
    }
    
    public string PassGenerate(User user)
    {
        var passHash = new PasswordHasher<User>();
        return passHash.HashPassword(user, "12345678");
    }
}