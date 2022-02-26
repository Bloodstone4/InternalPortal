using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication5.Migrations
{
    public partial class _ans1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
               migrationBuilder.CreateTable(
                name: "Ans1",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ans1", x => x.Id);
                });

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropTable(
                name: "Ans1");

            migrationBuilder.RenameColumn(
                name: "ans1Id",
                table: "Cors",
                newName: "ansId");

            migrationBuilder.RenameIndex(
                name: "IX_Cors_ans1Id",
                table: "Cors",
                newName: "IX_Cors_ansId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cors_AnsSet_ansId",
                table: "Cors",
                column: "ansId",
                principalTable: "AnsSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
