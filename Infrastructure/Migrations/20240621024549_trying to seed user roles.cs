using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tryingtoseeduserroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "AppDb",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dc043262-673a-491a-b811-446703743743",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "36890ad1-6eae-467e-b655-fbc0e3fe6b1d", "AQAAAAIAAYagAAAAEMlcIVlkUIpOBhTIphdRyJ9HcNZ818W7S97CIxi6Mnt+Yo++Ys6MlJn49JCd67Wkdg==", "6f8de872-4347-4e0a-a096-acf96a7e439d" });

            migrationBuilder.UpdateData(
                schema: "AppDb",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc043262-673a-491a-b811-446703743743"),
                column: "CreatedAt",
                value: new DateTime(2024, 6, 21, 2, 45, 49, 644, DateTimeKind.Utc).AddTicks(9300));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
