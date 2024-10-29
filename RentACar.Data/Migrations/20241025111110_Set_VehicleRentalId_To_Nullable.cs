using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
    /// <inheritdoc />
    public partial class Set_VehicleRentalId_To_Nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Rentals_RentalId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<Guid>(
                name: "RentalId",
                table: "Vehicles",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Id of the current rental for this car.",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Id of the current rental for this car.");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Vehicles",
                type: "nvarchar(2083)",
                maxLength: 2083,
                nullable: true,
                defaultValue: "/img/no-image.jpg",
                comment: "Url of the image of the car.",
                oldClrType: typeof(string),
                oldType: "nvarchar(2083)",
                oldMaxLength: 2083,
                oldNullable: true,
                oldComment: "Url of the image of the car.");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Rentals",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Rentals_RentalId",
                table: "Vehicles",
                column: "RentalId",
                principalTable: "Rentals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Rentals_RentalId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<Guid>(
                name: "RentalId",
                table: "Vehicles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Id of the current rental for this car.",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Id of the current rental for this car.");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Vehicles",
                type: "nvarchar(2083)",
                maxLength: 2083,
                nullable: true,
                comment: "Url of the image of the car.",
                oldClrType: typeof(string),
                oldType: "nvarchar(2083)",
                oldMaxLength: 2083,
                oldNullable: true,
                oldDefaultValue: "/img/no-image.jpg",
                oldComment: "Url of the image of the car.");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Rentals",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Rentals_RentalId",
                table: "Vehicles",
                column: "RentalId",
                principalTable: "Rentals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
