using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Islands.Migrations
{
    public partial class nevfixek : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Players_PlayerInformationId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "PlayerInformationId",
                table: "Notifications",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_PlayerInformationId",
                table: "Notifications",
                newName: "IX_Notifications_PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Players_PlayerId",
                table: "Notifications",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Players_PlayerId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "Notifications",
                newName: "PlayerInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_PlayerId",
                table: "Notifications",
                newName: "IX_Notifications_PlayerInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Players_PlayerInformationId",
                table: "Notifications",
                column: "PlayerInformationId",
                principalTable: "Players",
                principalColumn: "Id");
        }
    }
}
