using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication5.Migrations
{
    public partial class project_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Cors",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InternalNum = table.Column<string>(nullable: true),
                    ContractNumber = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    ManagerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectSet_Users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cors_ProjectId",
                table: "Cors",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSet_ManagerId",
                table: "ProjectSet",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cors_ProjectSet_ProjectId",
                table: "Cors",
                column: "ProjectId",
                principalTable: "ProjectSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cors_ProjectSet_ProjectId",
                table: "Cors");

            migrationBuilder.DropTable(
                name: "ProjectSet");

            migrationBuilder.DropIndex(
                name: "IX_Cors_ProjectId",
                table: "Cors");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Cors");
        }
    }
}
