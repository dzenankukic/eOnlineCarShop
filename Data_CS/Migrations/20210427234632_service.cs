using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_CS.Migrations
{
    public partial class service : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_ServicedCars_ServiceID",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicedCars_ServicePriceCriterias_ServicePriceCriteriaID",
                table: "ServicedCars");

            migrationBuilder.DropTable(
                name: "ServicePriceCriterias");

            migrationBuilder.DropIndex(
                name: "IX_ServicedCars_ServicePriceCriteriaID",
                table: "ServicedCars");

            migrationBuilder.DropIndex(
                name: "IX_Car_ServiceID",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "DateOfRegistraion",
                table: "ServicedCars");

            migrationBuilder.DropColumn(
                name: "ServicePriceCriteriaID",
                table: "ServicedCars");

            migrationBuilder.DropColumn(
                name: "ServiceID",
                table: "Car");

            migrationBuilder.AddColumn<int>(
                name: "CarID",
                table: "ServicedCars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateofServiceWarranty",
                table: "ServicedCars",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Recommendations",
                table: "ServicedCars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Warnings",
                table: "ServicedCars",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServicedCars_CarID",
                table: "ServicedCars",
                column: "CarID");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicedCars_Car_CarID",
                table: "ServicedCars",
                column: "CarID",
                principalTable: "Car",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicedCars_Car_CarID",
                table: "ServicedCars");

            migrationBuilder.DropIndex(
                name: "IX_ServicedCars_CarID",
                table: "ServicedCars");

            migrationBuilder.DropColumn(
                name: "CarID",
                table: "ServicedCars");

            migrationBuilder.DropColumn(
                name: "DateofServiceWarranty",
                table: "ServicedCars");

            migrationBuilder.DropColumn(
                name: "Recommendations",
                table: "ServicedCars");

            migrationBuilder.DropColumn(
                name: "Warnings",
                table: "ServicedCars");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfRegistraion",
                table: "ServicedCars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ServicePriceCriteriaID",
                table: "ServicedCars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceID",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ServicePriceCriterias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriteriaName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePriceCriterias", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServicedCars_ServicePriceCriteriaID",
                table: "ServicedCars",
                column: "ServicePriceCriteriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Car_ServiceID",
                table: "Car",
                column: "ServiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_ServicedCars_ServiceID",
                table: "Car",
                column: "ServiceID",
                principalTable: "ServicedCars",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicedCars_ServicePriceCriterias_ServicePriceCriteriaID",
                table: "ServicedCars",
                column: "ServicePriceCriteriaID",
                principalTable: "ServicePriceCriterias",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
