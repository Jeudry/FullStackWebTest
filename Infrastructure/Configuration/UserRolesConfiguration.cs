using Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public sealed class UserRolesConfiguration: IEntityTypeConfiguration<IdentityUserRole<string>>
{
    private const string AdminId = "dc043262-673a-491a-b811-446703743743";
    private const string UserId = "dc043262-673a-491a-b811-446703743744";
    private const string Admin2Id = "dc043262-673a-491a-b811-446703743745";
    private const string UserId2 = "dc043262-673a-491a-b811-446703743746";
    
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>()
            {
                RoleId = AdminId,
                UserId = AdminId
            },
            new IdentityUserRole<string>()
            {
                RoleId = UserId,
                UserId = UserId
            },
            new IdentityUserRole<string>()
            {
                RoleId = UserId,
                UserId = Admin2Id
            },
            new IdentityUserRole<string>()
            {
                RoleId = AdminId,
                UserId = UserId2
            }
        );
    }
}