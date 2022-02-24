using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication5.Migrations
{
    public partial class projectAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Project",
                table: "Cors");

            migrationBuilder.AlterColumn<string>(
                name: "InternalNum",
                table: "ProjectSet",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "ProjectSet",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InternalNum",
                table: "ProjectSet",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "ProjectSet",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Project",
                table: "Cors",
                nullable: true);
        }
    }
}
