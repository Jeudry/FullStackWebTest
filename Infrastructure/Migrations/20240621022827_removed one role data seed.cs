using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removedoneroledataseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { "7ffb7526-f864-4c06-ba5d-38219d7f3ba9", "AQAAAAIAAYagAAAAEDLnq54W7zei6uie4qinjBVArGijTl2rXqgbUp350U95twXAsMLOqFXhh0OywYFJ0A==", "ba54492d-7f7a-4c74-aabb-09f33bd051d8" });

            migrationBuilder.UpdateData(
                schema: "AppDb",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc043262-673a-491a-b811-446703743743"),
                column: "CreatedAt",
                value: new DateTime(2024, 6, 21, 2, 28, 27, 593, DateTimeKind.Utc).AddTicks(7980));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "AppDb",
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dc043262-673a-491a-b811-446703743744", "dc043262-673a-491a-b811-446703743743" });

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
    }
}
