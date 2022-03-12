using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WebApplication5.Migrations
{
    public partial class init_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResponseSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Text = table.Column<string>(nullable: true),
                    ImageLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    InternalNum = table.Column<string>(nullable: false),
                    ContractNumber = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: false),
                    ManagerId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ShowInMenuBar = table.Column<bool>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Cors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CorNumber = table.Column<int>(nullable: false),
                    CorTerm = table.Column<DateTime>(nullable: false),
                    CorBodyText = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ImageLink = table.Column<string>(nullable: false),
                    ExecutorId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true),
                    ResponseId = table.Column<int>(nullable: true),
                    RecieveDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cors_Users_ExecutorId",
                        column: x => x.ExecutorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cors_ProjectSet_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cors_ResponseSet_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "ResponseSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cors_ExecutorId",
                table: "Cors",
                column: "ExecutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cors_ProjectId",
                table: "Cors",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Cors_ResponseId",
                table: "Cors",
                column: "ResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSet_ManagerId",
                table: "ProjectSet",
                column: "ManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cors");

            migrationBuilder.DropTable(
                name: "ProjectSet");

            migrationBuilder.DropTable(
                name: "ResponseSet");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
