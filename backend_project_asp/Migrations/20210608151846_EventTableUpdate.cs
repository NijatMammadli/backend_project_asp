using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backend_project_asp.Migrations
{
    public partial class EventTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Events");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
