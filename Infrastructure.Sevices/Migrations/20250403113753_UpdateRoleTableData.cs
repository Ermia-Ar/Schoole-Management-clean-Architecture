using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoleTableData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5510348-12d0-422c-9912-3faad602e3e4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a71a55d6-99d7-4123-b4e0-1218ecb90e3e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c309fa92-2123-47be-b397-a1c77adb502c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "Admin", "Admin", "Admin", "ADMIN" },
                    { "Student", "Student", "Student", "STUDENT" },
                    { "Teacher", "Teacher", "Teacher", "TEACHER" }
                });

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Admin");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Student");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Teacher");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a5510348-12d0-422c-9912-3faad602e3e4", "a5510348-12d0-422c-9912-3faad602e3e4", "Student", "STUDENT" },
                    { "a71a55d6-99d7-4123-b4e0-1218ecb90e3e", "a71a55d6-99d7-4123-b4e0-1218ecb90e3e", "Admin", "ADMIN" },
                    { "c309fa92-2123-47be-b397-a1c77adb502c", "c309fa92-2123-47be-b397-a1c77adb502c", "Teacher", "WRITER" }
                });
        }
    }
}
