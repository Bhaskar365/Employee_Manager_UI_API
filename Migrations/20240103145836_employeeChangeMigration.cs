using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UI_API.Migrations
{
    public partial class employeeChangeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userImage",
                table: "Employee_Table");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userImage",
                table: "Employee_Table",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
