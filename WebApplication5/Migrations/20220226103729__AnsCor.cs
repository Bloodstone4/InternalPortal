using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication5.Migrations
{
    public partial class _AnsCor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnswerCorId",
                table: "Cors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cors_AnswerCorId",
                table: "Cors",
                column: "AnswerCorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cors_Answers_AnswerCorId",
                table: "Cors",
                column: "AnswerCorId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cors_Answers_AnswerCorId",
                table: "Cors");

            migrationBuilder.DropIndex(
                name: "IX_Cors_AnswerCorId",
                table: "Cors");

            migrationBuilder.DropColumn(
                name: "AnswerCorId",
                table: "Cors");
        }
    }
}
