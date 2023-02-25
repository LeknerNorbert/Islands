using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Islands.Migrations
{
    public partial class adtoexcahnge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Players_PlayerInformationId",
                table: "Buildings");

            migrationBuilder.DropTable(
                name: "Ads");

            migrationBuilder.RenameColumn(
                name: "PlayerInformationId",
                table: "Buildings",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Buildings_PlayerInformationId",
                table: "Buildings",
                newName: "IX_Buildings_PlayerId");

            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Item = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    ReplacementItem = table.Column<int>(type: "int", nullable: false),
                    ReplacementAmount = table.Column<int>(type: "int", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exchanges_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_PlayerId",
                table: "Exchanges",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Players_PlayerId",
                table: "Buildings",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Players_PlayerId",
                table: "Buildings");

            migrationBuilder.DropTable(
                name: "Exchanges");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "Buildings",
                newName: "PlayerInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_Buildings_PlayerId",
                table: "Buildings",
                newName: "IX_Buildings_PlayerInformationId");

            migrationBuilder.CreateTable(
                name: "Ads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PlayerInformationId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Item = table.Column<int>(type: "int", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ReplacementAmount = table.Column<int>(type: "int", nullable: false),
                    ReplacementItem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ads_Players_PlayerInformationId",
                        column: x => x.PlayerInformationId,
                        principalTable: "Players",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Ads_PlayerInformationId",
                table: "Ads",
                column: "PlayerInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Players_PlayerInformationId",
                table: "Buildings",
                column: "PlayerInformationId",
                principalTable: "Players",
                principalColumn: "Id");
        }
    }
}
