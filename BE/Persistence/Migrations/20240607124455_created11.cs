using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class created11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
