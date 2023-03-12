using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Islands.Migrations
{
    public partial class abilitytoagility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ability",
                table: "Players",
                newName: "Agility");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Agility",
                table: "Players",
                newName: "Ability");
        }
    }
}
