using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRangesToVehicleYearProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Vehicles",
                type: "int",
                nullable: false,
                comment: "Year of manufacturing.",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Year of manufacturing.");

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("8a6fbd63-0c0e-4b82-a5c5-c50f9de6ec12"),
                column: "Year",
                value: 2022);

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("c59b3736-645b-471b-9c0b-205792c82b8e"),
                column: "Year",
                value: 2022);

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("d9e0f547-3b8e-42a6-a8a5-6c74ec9b0154"),
                column: "Year",
                value: 2021);

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("e6c4c94a-8c92-44f4-a213-7bdbf3e5a57f"),
                column: "Year",
                value: 2020);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Year",
                table: "Vehicles",
                type: "datetime2",
                nullable: false,
                comment: "Year of manufacturing.",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Year of manufacturing.");

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("8a6fbd63-0c0e-4b82-a5c5-c50f9de6ec12"),
                column: "Year",
                value: new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("c59b3736-645b-471b-9c0b-205792c82b8e"),
                column: "Year",
                value: new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("d9e0f547-3b8e-42a6-a8a5-6c74ec9b0154"),
                column: "Year",
                value: new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("e6c4c94a-8c92-44f4-a213-7bdbf3e5a57f"),
                column: "Year",
                value: new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
