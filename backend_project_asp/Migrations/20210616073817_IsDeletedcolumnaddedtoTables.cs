using Microsoft.EntityFrameworkCore.Migrations;

namespace backend_project_asp.Migrations
{
    public partial class IsDeletedcolumnaddedtoTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Subscribes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EventDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CourseDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BlogDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Subscribes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EventDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CourseDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BlogDetails");
        }
    }
}
