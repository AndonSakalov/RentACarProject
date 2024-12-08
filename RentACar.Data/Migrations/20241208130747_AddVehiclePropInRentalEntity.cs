using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddVehiclePropInRentalEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Rentals_RentalId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_RentalId",
                table: "Vehicles");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_RentalId",
                table: "Vehicles",
                column: "RentalId",
                unique: true,
                filter: "[RentalId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Rentals_RentalId",
                table: "Vehicles",
                column: "RentalId",
                principalTable: "Rentals",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Rentals_RentalId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_RentalId",
                table: "Vehicles");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_RentalId",
                table: "Vehicles",
                column: "RentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Rentals_RentalId",
                table: "Vehicles",
                column: "RentalId",
                principalTable: "Rentals",
                principalColumn: "Id");
        }
    }
}
