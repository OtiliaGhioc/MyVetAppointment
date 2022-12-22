using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetAppointment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyDrugAndStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Drugs");

            migrationBuilder.AddColumn<int>(
                name: "PricePerItem",
                table: "DrugStocks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerItem",
                table: "DrugStocks");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Drugs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
