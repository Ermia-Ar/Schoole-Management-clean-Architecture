using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeacherCourses_CourseId",
                table: "TeacherCourses");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TeacherCourses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "StudentCourses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourses_CourseId",
                table: "TeacherCourses",
                column: "CourseId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeacherCourses_CourseId",
                table: "TeacherCourses");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TeacherCourses");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "StudentCourses");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Courses");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourses_CourseId",
                table: "TeacherCourses",
                column: "CourseId");
        }
    }
}
