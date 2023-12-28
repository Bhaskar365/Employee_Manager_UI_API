using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UI_API.Migrations
{
    public partial class newCommitEmployeeParameterChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Employee_Table",
                newName: "Country");

            migrationBuilder.AlterColumn<string>(
                name: "DateOfHire",
                table: "Employee_Table",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedOn",
                table: "Employee_Table",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Employee_Table",
                newName: "Address");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfHire",
                table: "Employee_Table",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Employee_Table",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
