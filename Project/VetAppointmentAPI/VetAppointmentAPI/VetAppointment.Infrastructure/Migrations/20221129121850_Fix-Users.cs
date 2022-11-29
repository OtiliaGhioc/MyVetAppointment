using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetAppointment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Offices_UserOfficeOfficeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserOfficeOfficeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserOfficeOfficeId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "isExpired",
                table: "Appointments",
                newName: "IsExpired");

            migrationBuilder.AddColumn<Guid>(
                name: "OfficeId",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_OfficeId",
                table: "Users",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Offices_OfficeId",
                table: "Users",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "OfficeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Offices_OfficeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_OfficeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "IsExpired",
                table: "Appointments",
                newName: "isExpired");

            migrationBuilder.AddColumn<Guid>(
                name: "UserOfficeOfficeId",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserOfficeOfficeId",
                table: "Users",
                column: "UserOfficeOfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Offices_UserOfficeOfficeId",
                table: "Users",
                column: "UserOfficeOfficeId",
                principalTable: "Offices",
                principalColumn: "OfficeId");
        }
    }
}
