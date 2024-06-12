using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class craetedb15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "8a0d6a77-bc47-473b-bcf7-f0d001fa3076", null, "User", "USER" },
                    { "e40ac3ec-130b-49d0-aa70-aa27100ca3ec", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c5a71cea-df8e-4e0e-ac02-e3704baecf20", 0, null, "ff648705-d8fc-4bab-b87f-6eb9646fb024", "huutainguyentran96@gmail.com", true, null, false, null, "HUUTAINGUYENTRAN96@GMAIL.COM", "ABCXYZ", "AQAAAAIAAYagAAAAEJox2+MTt3PPFxcUeVCuF/USIixk68mb7cAYnPa0I9KfubbXn3PtbJqa1Q2TkTkWBg==", null, false, "4de3d87e-e304-458a-a1a1-f2db536128d7", false, "abcxyz" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e40ac3ec-130b-49d0-aa70-aa27100ca3ec", "c5a71cea-df8e-4e0e-ac02-e3704baecf20" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a0d6a77-bc47-473b-bcf7-f0d001fa3076");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e40ac3ec-130b-49d0-aa70-aa27100ca3ec", "c5a71cea-df8e-4e0e-ac02-e3704baecf20" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e40ac3ec-130b-49d0-aa70-aa27100ca3ec");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c5a71cea-df8e-4e0e-ac02-e3704baecf20");

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
    }
}
