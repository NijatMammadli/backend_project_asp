using Microsoft.EntityFrameworkCore.Migrations;

namespace backend_project_asp.Migrations
{
    public partial class UserDeActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeactive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeactive",
                table: "AspNetUsers");
        }
    }
}
