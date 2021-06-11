using Microsoft.EntityFrameworkCore.Migrations;

namespace backend_project_asp.Migrations
{
    public partial class updateNoticeBoard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeaderVideo",
                table: "NoticeBoards");

            migrationBuilder.DropColumn(
                name: "LinkVideo",
                table: "NoticeBoards");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HeaderVideo",
                table: "NoticeBoards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkVideo",
                table: "NoticeBoards",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
