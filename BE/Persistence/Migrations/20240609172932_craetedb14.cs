using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class craetedb14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb71d3de-4ca5-47f8-9557-65294d5c8067");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "343b721e-6962-4030-a61b-2404f619b9a4", "457e8ff6-f6e1-480a-99b6-31377931c2ef" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "343b721e-6962-4030-a61b-2404f619b9a4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "457e8ff6-f6e1-480a-99b6-31377931c2ef");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "14b174db-e01e-4a30-bea2-9df0f4cbd20d", null, "User", "USER" },
                    { "d1f000b2-d3eb-41d5-b209-b10cd9d592b5", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "35a2a4a6-984c-4205-b856-3d42cce4e35d", 0, null, "6a95410b-a108-4a45-84c7-32b13d49d0bb", "huutainguyentran96@gmail.com", true, null, false, null, "HUUTAINGUYENTRAN96@GMAIL.COM", "ADMINISTRATOR", "AQAAAAIAAYagAAAAEPYXhdwaP0iW+DL0LgvLIZNpe5X4GDIrtoAldQfnlN7tc5VZ0N0NDph6+Q3Xx9FOtQ==", null, false, "7f6cf911-2790-4f79-9f7a-a602bbde88fb", false, "administrator" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d1f000b2-d3eb-41d5-b209-b10cd9d592b5", "35a2a4a6-984c-4205-b856-3d42cce4e35d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14b174db-e01e-4a30-bea2-9df0f4cbd20d");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d1f000b2-d3eb-41d5-b209-b10cd9d592b5", "35a2a4a6-984c-4205-b856-3d42cce4e35d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1f000b2-d3eb-41d5-b209-b10cd9d592b5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "35a2a4a6-984c-4205-b856-3d42cce4e35d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "343b721e-6962-4030-a61b-2404f619b9a4", null, "Admin", "ADMIN" },
                    { "eb71d3de-4ca5-47f8-9557-65294d5c8067", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "457e8ff6-f6e1-480a-99b6-31377931c2ef", 0, null, "1b292bbd-e1f5-4e44-adaa-9ee873cc9d01", "huutainguyentran96@gmail.com", true, null, false, null, "HUUTAINGUYENTRAN96@GMAIL.COM", "HUUTAI", "AQAAAAIAAYagAAAAEFaHxoYZ6EHb9c2u8M1KQ1N2VPIDPlZ7GJk3pSxm81bhfwtVUroXvBE8z6urjYfmYg==", null, false, "967d1211-4383-4e14-8157-df305496d5df", false, "huutai" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "343b721e-6962-4030-a61b-2404f619b9a4", "457e8ff6-f6e1-480a-99b6-31377931c2ef" });
        }
    }
}
