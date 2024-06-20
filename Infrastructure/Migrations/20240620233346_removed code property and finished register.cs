using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removedcodepropertyandfinishedregister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Code",
                schema: "AppDb",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "AppDb",
                table: "Products");

            migrationBuilder.UpdateData(
                schema: "AppDb",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dc043262-673a-491a-b811-446703743743",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "12714d0c-ba6e-4315-b6be-9a30c3789f26", "4c5a42e4-fe87-4f10-8441-501090657e2c" });

            migrationBuilder.UpdateData(
                schema: "AppDb",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc043262-673a-491a-b811-446703743743"),
                column: "CreatedAt",
                value: new DateTime(2024, 6, 20, 23, 33, 46, 218, DateTimeKind.Utc).AddTicks(5790));

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                schema: "AppDb",
                table: "Products",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                schema: "AppDb",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "AppDb",
                table: "Products",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "AppDb",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dc043262-673a-491a-b811-446703743743",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "07fd95fc-7916-4c0d-bc20-1ba60ece5920", "55673a48-1a55-49c6-bd13-068861e13be5" });

            migrationBuilder.UpdateData(
                schema: "AppDb",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc043262-673a-491a-b811-446703743743"),
                columns: new[] { "Code", "CreatedAt" },
                values: new object[] { "PROD", new DateTime(2024, 6, 20, 17, 35, 34, 176, DateTimeKind.Utc).AddTicks(20) });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Code",
                schema: "AppDb",
                table: "Products",
                column: "Code",
                unique: true);
        }
    }
}
