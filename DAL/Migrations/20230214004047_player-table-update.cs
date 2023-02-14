using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class playertableupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_PlayerInformations_PlayerInformationId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Classifieds_PlayerInformations_PlayerInformationId",
                table: "Classifieds");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_PlayerInformations_PlayerInformationId",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "PlayerInformations");

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ExperiencePoint = table.Column<int>(type: "int", nullable: false),
                    Coins = table.Column<int>(type: "int", nullable: false),
                    Woods = table.Column<int>(type: "int", nullable: false),
                    Stones = table.Column<int>(type: "int", nullable: false),
                    Irons = table.Column<int>(type: "int", nullable: false),
                    SelectedIsland = table.Column<int>(type: "int", nullable: false),
                    LastExpeditionDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastBattleDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Strength = table.Column<int>(type: "int", nullable: false),
                    Intelligence = table.Column<int>(type: "int", nullable: false),
                    Ability = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Players_PlayerInformationId",
                table: "Buildings",
                column: "PlayerInformationId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classifieds_Players_PlayerInformationId",
                table: "Classifieds",
                column: "PlayerInformationId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Players_PlayerInformationId",
                table: "Notifications",
                column: "PlayerInformationId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Players_PlayerInformationId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Classifieds_Players_PlayerInformationId",
                table: "Classifieds");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Players_PlayerInformationId",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.CreateTable(
                name: "PlayerInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Ability = table.Column<int>(type: "int", nullable: false),
                    Coins = table.Column<int>(type: "int", nullable: false),
                    ExperiencePoint = table.Column<int>(type: "int", nullable: false),
                    Intelligence = table.Column<int>(type: "int", nullable: false),
                    Irons = table.Column<int>(type: "int", nullable: false),
                    LastBattleDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastExpeditionDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SelectedIsland = table.Column<int>(type: "int", nullable: false),
                    Stones = table.Column<int>(type: "int", nullable: false),
                    Strength = table.Column<int>(type: "int", nullable: false),
                    Woods = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerInformations_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_PlayerInformations_PlayerInformationId",
                table: "Buildings",
                column: "PlayerInformationId",
                principalTable: "PlayerInformations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classifieds_PlayerInformations_PlayerInformationId",
                table: "Classifieds",
                column: "PlayerInformationId",
                principalTable: "PlayerInformations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_PlayerInformations_PlayerInformationId",
                table: "Notifications",
                column: "PlayerInformationId",
                principalTable: "PlayerInformations",
                principalColumn: "Id");
        }
    }
}
