using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsRentedPropertyToVehicleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRented",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRented",
                table: "Vehicles");
        }
    }
}
