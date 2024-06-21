using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class userseed : Migration
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
                values: new object[] { "3d23e958-ec86-41ea-9f3b-d3f1adf9593d", "AQAAAAIAAYagAAAAELFbFa64k30THemtyYDoyEZi4BvWgpsXsAd5L1zNUcHD/nO8q/4eQNMt3DANe27qZg==", "342cb56d-e60c-4018-9d8b-c02a8f122863" });

            migrationBuilder.UpdateData(
                schema: "AppDb",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc043262-673a-491a-b811-446703743743"),
                column: "CreatedAt",
                value: new DateTime(2024, 6, 21, 2, 23, 23, 462, DateTimeKind.Utc));
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
                values: new object[] { "12714d0c-ba6e-4315-b6be-9a30c3789f26", null, "4c5a42e4-fe87-4f10-8441-501090657e2c" });

            migrationBuilder.UpdateData(
                schema: "AppDb",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc043262-673a-491a-b811-446703743743"),
                column: "CreatedAt",
                value: new DateTime(2024, 6, 20, 23, 33, 46, 218, DateTimeKind.Utc).AddTicks(5790));
        }
    }
}
