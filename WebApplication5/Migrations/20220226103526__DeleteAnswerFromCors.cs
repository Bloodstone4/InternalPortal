using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication5.Migrations
{
    public partial class _DeleteAnswerFromCors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cors_Answers_AnswerId",
                table: "Cors");

            migrationBuilder.DropIndex(
                name: "IX_Cors_AnswerId",
                table: "Cors");

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "Cors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnswerId",
                table: "Cors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cors_AnswerId",
                table: "Cors",
                column: "AnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cors_Answers_AnswerId",
                table: "Cors",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
