using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedNewDataSeed : Migration
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
                values: new object[] { "66f396db-fcf3-4001-99e4-2f608b5bc27b", "AQAAAAIAAYagAAAAELniJoL4XB9zJP3Lw3nPrcbnpJATDgi3ednHR/XA6X5y8H/+VFxG8fqm/SuFobK3dg==", "00c53988-e683-4881-8246-712ed05e49d6" });

            migrationBuilder.InsertData(
                schema: "AppDb",
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "dc043262-673a-491a-b811-446703743744", 0, "b4627c97-6463-4653-8a2f-e566d721cd73", "user@example.com", true, false, null, "USER@EXAMPLE.COM", "USER", "AQAAAAIAAYagAAAAEK7f7ykX61lD01YigVD9Z5ijhdjYXowqM9kmOn8kmlPbDa/2eTQtATN/Td310DrVfQ==", "18497505945", true, "11830716-3b3e-4714-91e1-be31bd5aea39", false, "User" },
                    { "dc043262-673a-491a-b811-446703743745", 0, "fdea4919-ce4a-4e54-8078-1d03076d9a8f", "admin2@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN2", "AQAAAAIAAYagAAAAEOP3lZM5Pw8NG5WjNcM6EllaviC0JH8oEDwqJZgosEKobDpPEUHznYtALMrIShYfdw==", "18497505936", true, "7b50bc0a-1b4c-4584-8ef9-30c953d955fa", false, "Admin2" },
                    { "dc043262-673a-491a-b811-446703743746", 0, "ec09ead2-4799-404e-a89b-3695435cf25b", "user2@example.com", true, false, null, "USER@EXAMPLE.COM", "USER2", "AQAAAAIAAYagAAAAEOjhE85YpTpXiDEfh+TB2FrT86lPFh+IbHt67G3HUPCqNgT1vcyi8B+e41tWfG6Gpg==", "18497505937", true, "042101a4-9b35-479d-bcc3-5fc45147266e", false, "User2" }
                });

            migrationBuilder.UpdateData(
                schema: "AppDb",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc043262-673a-491a-b811-446703743743"),
                columns: new[] { "CreatedAt", "Description", "Name" },
                values: new object[] { new DateTime(2024, 6, 21, 13, 25, 7, 836, DateTimeKind.Utc).AddTicks(3620), "Its a chair to sit", "Chair" });

            migrationBuilder.InsertData(
                schema: "AppDb",
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "Price", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("dc043262-673a-491a-b811-446703743744"), new DateTime(2024, 6, 21, 13, 25, 7, 836, DateTimeKind.Utc).AddTicks(3640), "Its a table to put things on", "Table", 200.0, 20, null },
                    { new Guid("dc043262-673a-491a-b811-446703743745"), new DateTime(2024, 6, 21, 13, 25, 7, 836, DateTimeKind.Utc).AddTicks(3650), "Its a sofa to sit", "Sofa", 300.0, 30, null },
                    { new Guid("dc043262-673a-491a-b811-446703743746"), new DateTime(2024, 6, 21, 13, 25, 7, 836, DateTimeKind.Utc).AddTicks(3650), "Its a bed to sleep", "Bed", 400.0, 40, null },
                    { new Guid("dc043262-673a-491a-b811-446703743747"), new DateTime(2024, 6, 21, 13, 25, 7, 836, DateTimeKind.Utc).AddTicks(3650), "Its a lamp to light", "Lamp", 500.0, 50, null },
                    { new Guid("dc043262-673a-491a-b811-446703743748"), new DateTime(2024, 6, 21, 13, 25, 7, 836, DateTimeKind.Utc).AddTicks(3650), "Its a curtain to cover", "Curtains", 600.0, 60, null },
                    { new Guid("dc043262-673a-491a-b811-446703743749"), new DateTime(2024, 6, 21, 13, 25, 7, 836, DateTimeKind.Utc).AddTicks(3650), "Its a carpet to walk", "Carpet", 700.0, 70, null },
                    { new Guid("dc043262-673a-491a-b811-446703743750"), new DateTime(2024, 6, 21, 13, 25, 7, 836, DateTimeKind.Utc).AddTicks(3660), "Its a painting to see", "Painting", 800.0, 80, null },
                    { new Guid("dc043262-673a-491a-b811-446703743751"), new DateTime(2024, 6, 21, 13, 25, 7, 836, DateTimeKind.Utc).AddTicks(3660), "Its a mirror to reflect", "Mirror", 900.0, 90, null },
                    { new Guid("dc043262-673a-491a-b811-446703743752"), new DateTime(2024, 6, 21, 13, 25, 7, 836, DateTimeKind.Utc).AddTicks(3660), "Its a vase to hold", "Vase", 1000.0, 100, null }
                });

            migrationBuilder.InsertData(
                schema: "AppDb",
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "dc043262-673a-491a-b811-446703743744", "dc043262-673a-491a-b811-446703743744" },
                    { "dc043262-673a-491a-b811-446703743744", "dc043262-673a-491a-b811-446703743745" },
                    { "dc043262-673a-491a-b811-446703743743", "dc043262-673a-491a-b811-446703743746" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dc043262-673a-491a-b811-446703743744", "dc043262-673a-491a-b811-446703743744" });

            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dc043262-673a-491a-b811-446703743744", "dc043262-673a-491a-b811-446703743745" });

            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dc043262-673a-491a-b811-446703743743", "dc043262-673a-491a-b811-446703743746" });

            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc043262-673a-491a-b811-446703743744"));

            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc043262-673a-491a-b811-446703743745"));

            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc043262-673a-491a-b811-446703743746"));

            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc043262-673a-491a-b811-446703743747"));

            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc043262-673a-491a-b811-446703743748"));

            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc043262-673a-491a-b811-446703743749"));

            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc043262-673a-491a-b811-446703743750"));

            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc043262-673a-491a-b811-446703743751"));

            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc043262-673a-491a-b811-446703743752"));

            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dc043262-673a-491a-b811-446703743744");

            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dc043262-673a-491a-b811-446703743745");

            migrationBuilder.DeleteData(
                schema: "AppDb",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dc043262-673a-491a-b811-446703743746");

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
                columns: new[] { "CreatedAt", "Description", "Name" },
                values: new object[] { new DateTime(2024, 6, 21, 2, 45, 49, 644, DateTimeKind.Utc).AddTicks(9300), "Product description", "Product" });
        }
    }
}
