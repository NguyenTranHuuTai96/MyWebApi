using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class craetedb13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8723fcb-b73a-4f94-8737-9b140fe6e272");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "43a79e98-f8da-4de3-84b2-c62fc8ebc794", "ffeeaede-3cc9-4ac3-a655-5e3ebc56a08f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43a79e98-f8da-4de3-84b2-c62fc8ebc794");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffeeaede-3cc9-4ac3-a655-5e3ebc56a08f");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "43a79e98-f8da-4de3-84b2-c62fc8ebc794", null, "Admin", "ADMIN" },
                    { "f8723fcb-b73a-4f94-8737-9b140fe6e272", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ffeeaede-3cc9-4ac3-a655-5e3ebc56a08f", 0, null, "9e195a00-8274-40f7-8684-644eb25c3a12", "huutainguyentran96@gmail.com", false, null, false, null, "HUUTAINGUYENTRAN96@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEFAjoPNL6b6RBXJcPVDmqyIFFRv7iW5eLnjGNOHQY5q+FTrHAC0iJUL5EUjk1sdDYA==", null, false, "459ce849-b2ef-4239-a762-2a2607b60764", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "43a79e98-f8da-4de3-84b2-c62fc8ebc794", "ffeeaede-3cc9-4ac3-a655-5e3ebc56a08f" });
        }
    }
}
