using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                schema: "AppDb",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "AppDb",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "AppDb",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "AppDb",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "AppDb",
                table: "Products",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                schema: "AppDb",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                schema: "AppDb",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "AppDb",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "AppDb",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "dc043262-673a-491a-b811-446703743743", null, "Admin", "ADMIN" },
                    { "dc043262-673a-491a-b811-446703743744", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                schema: "AppDb",
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "dc043262-673a-491a-b811-446703743743", 0, "07fd95fc-7916-4c0d-bc20-1ba60ece5920", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", null, "18497505944", true, "55673a48-1a55-49c6-bd13-068861e13be5", false, "Admin" });

            migrationBuilder.InsertData(
                schema: "AppDb",
                table: "Products",
                columns: new[] { "Id", "Code", "CreatedAt", "Description", "Name", "Price", "Stock", "UpdatedAt" },
                values: new object[] { new Guid("dc043262-673a-491a-b811-446703743743"), "PROD", new DateTime(2024, 6, 20, 17, 35, 34, 176, DateTimeKind.Utc).AddTicks(20), "Product description", "Product", 100.0, 10, null });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Code",
                schema: "AppDb",
                table: "Products",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Code",
                schema: "AppDb",
                table: "Products");

            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc043262-673a-491a-b811-446703743743");

            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc043262-673a-491a-b811-446703743744");

            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dc043262-673a-491a-b811-446703743743");

            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc043262-673a-491a-b811-446703743743"));

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "AppDb",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "AppDb",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "AppDb",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Stock",
                schema: "AppDb",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "AppDb",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "AppDb",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "AppDb",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                schema: "AppDb",
                table: "Products",
                column: "Name",
                unique: true);
        }
    }
}
