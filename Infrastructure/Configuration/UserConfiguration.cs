using Domain.Product;
using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

/// <summary>
/// Represents the user configuration.
/// </summary>
public sealed class UserConfiguration: IEntityTypeConfiguration<User>
{
    private const string AdminId = "dc043262-673a-491a-b811-446703743743";
    
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.HasData(
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
            }
        );

    }
}