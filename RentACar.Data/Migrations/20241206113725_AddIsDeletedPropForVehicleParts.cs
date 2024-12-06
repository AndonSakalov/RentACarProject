using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
	/// <inheritdoc />
	public partial class AddIsDeletedPropForVehicleParts : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<bool>(
				name: "IsDeleted",
				table: "Transmissions",
				type: "bit",
				nullable: false,
				defaultValue: false);

			migrationBuilder.AddColumn<bool>(
				name: "IsDeleted",
				table: "Makes",
				type: "bit",
				nullable: false,
				defaultValue: false);

			migrationBuilder.AddColumn<bool>(
				name: "IsDeleted",
				table: "Engines",
				type: "bit",
				nullable: false,
				defaultValue: false);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "IsDeleted",
				table: "Transmissions");

			migrationBuilder.DropColumn(
				name: "IsDeleted",
				table: "Makes");

			migrationBuilder.DropColumn(
				name: "IsDeleted",
				table: "Engines");
		}
	}
}
