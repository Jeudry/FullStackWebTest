using Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

/// <summary>
/// Represents the role configuration.
/// </summary>
public sealed class RoleConfiguration: IEntityTypeConfiguration<IdentityRole>
{
    private const string AdminId = "dc043262-673a-491a-b811-446703743743";
    private const string UserId = "dc043262-673a-491a-b811-446703743744";
    
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = UserId,
                Name = User.DefaultRole,
                NormalizedName = User.DefaultRole.ToUpper()
            },
            new IdentityRole
            {
                Id = AdminId,
                Name = User.AdminRole,
                NormalizedName = User.AdminRole.ToUpper()
            }
        );
    }
}