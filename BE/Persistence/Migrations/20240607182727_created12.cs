using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class created12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5e50bc7-f488-4d0a-a854-09bac5a8d35e");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6062ccb1-bba2-4280-a28e-181b185b8f59", "a6bb7689-131c-4e91-8ba3-fa5d31b450fc" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6062ccb1-bba2-4280-a28e-181b185b8f59");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a6bb7689-131c-4e91-8ba3-fa5d31b450fc");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "6062ccb1-bba2-4280-a28e-181b185b8f59", null, "Admin", "Admin" },
                    { "f5e50bc7-f488-4d0a-a854-09bac5a8d35e", null, "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a6bb7689-131c-4e91-8ba3-fa5d31b450fc", 0, null, "15d761d2-3f7a-40c3-9465-61c0627988df", "huutainguyentran96@gmail.com", false, null, false, null, "huutainguyentran96@gmail.com", "admin", "AQAAAAIAAYagAAAAEGpvISP8Ilq+YkxsZJSyNd4iW+M0+ashi98b59Zd3Xe4jIqAoJJU0chw2AiSKmkHiA==", null, false, "67627984-d26f-4fd3-8a21-adca643389e9", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6062ccb1-bba2-4280-a28e-181b185b8f59", "a6bb7689-131c-4e91-8ba3-fa5d31b450fc" });
        }
    }
}
