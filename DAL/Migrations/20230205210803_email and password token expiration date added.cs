using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class emailandpasswordtokenexpirationdateadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValidationToken",
                table: "Users",
                newName: "PasswordResetToken");

            migrationBuilder.RenameColumn(
                name: "ValidationDate",
                table: "Users",
                newName: "PasswordResetTokenExpiration");

            migrationBuilder.RenameColumn(
                name: "ResetToken",
                table: "Users",
                newName: "EmailValidationToken");

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailValidationTokenExpiration",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailValid",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailValidationTokenExpiration",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsEmailValid",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PasswordResetTokenExpiration",
                table: "Users",
                newName: "ValidationDate");

            migrationBuilder.RenameColumn(
                name: "PasswordResetToken",
                table: "Users",
                newName: "ValidationToken");

            migrationBuilder.RenameColumn(
                name: "EmailValidationToken",
                table: "Users",
                newName: "ResetToken");
        }
    }
}
