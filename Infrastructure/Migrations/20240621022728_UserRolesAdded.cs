using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserRolesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "AppDb",
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "dc043262-673a-491a-b811-446703743743", "dc043262-673a-491a-b811-446703743743" },
                    { "dc043262-673a-491a-b811-446703743744", "dc043262-673a-491a-b811-446703743743" }
                });

            migrationBuilder.UpdateData(
                schema: "AppDb",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dc043262-673a-491a-b811-446703743743",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d3703e1-a750-4c2f-a572-de791059c363", "AQAAAAIAAYagAAAAEFzycSzMvKEg4Jr2ryYxKbk7Iv5hSVUW0+7kvVJfN5Vx6oNBKCoxzoey3tymxHSR+A==", "8d8a59cc-fd64-4388-86ee-be6949c3cf28" });

            migrationBuilder.UpdateData(
                schema: "AppDb",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc043262-673a-491a-b811-446703743743"),
                column: "CreatedAt",
                value: new DateTime(2024, 6, 21, 2, 27, 28, 502, DateTimeKind.Utc).AddTicks(950));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dc043262-673a-491a-b811-446703743743", "dc043262-673a-491a-b811-446703743743" });

            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dc043262-673a-491a-b811-446703743744", "dc043262-673a-491a-b811-446703743743" });

            migrationBuilder.UpdateData(
                schema: "AppDb",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dc043262-673a-491a-b811-446703743743",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d23e958-ec86-41ea-9f3b-d3f1adf9593d", "AQAAAAIAAYagAAAAELFbFa64k30THemtyYDoyEZi4BvWgpsXsAd5L1zNUcHD/nO8q/4eQNMt3DANe27qZg==", "342cb56d-e60c-4018-9d8b-c02a8f122863" });

            migrationBuilder.UpdateData(
                schema: "AppDb",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc043262-673a-491a-b811-446703743743"),
                column: "CreatedAt",
                value: new DateTime(2024, 6, 21, 2, 23, 23, 462, DateTimeKind.Utc));
        }
    }
}
