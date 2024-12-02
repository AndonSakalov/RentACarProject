using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
	/// <inheritdoc />
	public partial class AddReservationEntity : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Reservation",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					PickUpDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The pick up date for the reserved vehicle."),
					ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The return date for the reserved vehicle."),
					Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "The price for the reserved rental.")
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Reservation", x => x.Id);
					table.ForeignKey(
						name: "FK_Reservation_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
					table.ForeignKey(
						name: "FK_Reservation_Vehicles_VehicleId",
						column: x => x.VehicleId,
						principalTable: "Vehicles",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Reservation_UserId",
				table: "Reservation",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_Reservation_VehicleId",
				table: "Reservation",
				column: "VehicleId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Reservation");
		}
	}
}
