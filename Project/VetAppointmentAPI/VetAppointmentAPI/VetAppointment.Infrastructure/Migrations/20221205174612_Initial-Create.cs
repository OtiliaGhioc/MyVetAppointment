using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetAppointment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drugs",
                columns: table => new
                {
                    DrugId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drugs", x => x.DrugId);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    OfficeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.OfficeId);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    PrescriptionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.PrescriptionId);
                });

            migrationBuilder.CreateTable(
                name: "DrugStocks",
                columns: table => new
                {
                    DrugStockId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TypeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugStocks", x => x.DrugStockId);
                    table.ForeignKey(
                        name: "FK_DrugStocks_Drugs_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Drugs",
                        principalColumn: "DrugId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    IsMedic = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasOffice = table.Column<bool>(type: "INTEGER", nullable: false),
                    OfficeId = table.Column<Guid>(type: "TEXT", nullable: true),
                    JoinedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "OfficeId");
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionDrugs",
                columns: table => new
                {
                    PrescriptionDrugId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StockId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    PrescriptionId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionDrugs", x => x.PrescriptionDrugId);
                    table.ForeignKey(
                        name: "FK_PrescriptionDrugs_DrugStocks_StockId",
                        column: x => x.StockId,
                        principalTable: "DrugStocks",
                        principalColumn: "DrugStockId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescriptionDrugs_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "PrescriptionId");
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AppointerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AppointeeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsExpired = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_AppointeeId",
                        column: x => x.AppointeeId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_AppointerId",
                        column: x => x.AppointerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillingEntries",
                columns: table => new
                {
                    BillingEntryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IssuerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PrescriptionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingEntries", x => x.BillingEntryId);
                    table.ForeignKey(
                        name: "FK_BillingEntries_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillingEntries_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "PrescriptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillingEntries_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillingEntries_Users_IssuerId",
                        column: x => x.IssuerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalEntries",
                columns: table => new
                {
                    MedicalHistoryEntryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PrescriptionId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalEntries", x => x.MedicalHistoryEntryId);
                    table.ForeignKey(
                        name: "FK_MedicalEntries_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalEntries_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "PrescriptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointeeId",
                table: "Appointments",
                column: "AppointeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointerId",
                table: "Appointments",
                column: "AppointerId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingEntries_AppointmentId",
                table: "BillingEntries",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingEntries_CustomerId",
                table: "BillingEntries",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingEntries_IssuerId",
                table: "BillingEntries",
                column: "IssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingEntries_PrescriptionId",
                table: "BillingEntries",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugStocks_TypeId",
                table: "DrugStocks",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalEntries_AppointmentId",
                table: "MedicalEntries",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalEntries_PrescriptionId",
                table: "MedicalEntries",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionDrugs_PrescriptionId",
                table: "PrescriptionDrugs",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionDrugs_StockId",
                table: "PrescriptionDrugs",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OfficeId",
                table: "Users",
                column: "OfficeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingEntries");

            migrationBuilder.DropTable(
                name: "MedicalEntries");

            migrationBuilder.DropTable(
                name: "PrescriptionDrugs");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "DrugStocks");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Drugs");

            migrationBuilder.DropTable(
                name: "Offices");
        }
    }
}
