using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddImagesCollumnToVehicleType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "VehicleTypes",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "/img/no-image.jpg",
                comment: "The image url of the vehicle type.");

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("2f60f64b-3c93-466f-b0f1-7b4005b5c75f"),
                column: "ImageUrl",
                value: "/img/sportscar-img.jpg");

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("4b8bfe96-e6b2-41e6-b14c-bf2e5d3a49a0"),
                column: "ImageUrl",
                value: "/img/sedan-img.jpg");

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("5c6c3d85-4a4c-4b9f-a0b4-fd6d6b21a38f"),
                column: "ImageUrl",
                value: "/img/stationwagon-img.jpg");

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("6f1b99c8-749f-4cf7-a30c-5d576b5098e6"),
                column: "ImageUrl",
                value: "/img/hatchback-img.jpg");

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("7b9c1cc8-0f3a-4b9f-8517-1b798e63f8b1"),
                column: "ImageUrl",
                value: "/img/crossover-img.jpg");

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("83e0b3af-1f02-4b68-8e3b-87d469243024"),
                column: "ImageUrl",
                value: "/img/coupe-img.jpg");

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("8f5b0e36-d27a-4cf2-a22d-06c24094c62a"),
                column: "ImageUrl",
                value: "/img/jeep-img.jpg");

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("97e1e204-7990-4c76-9b9f-4d60e5e2a44f"),
                column: "ImageUrl",
                value: "/img/musclecar-img.jpg");

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("bbab29d3-b20a-42c8-b7b8-0b012f2d186b"),
                column: "ImageUrl",
                value: "/img/limousine-img.jpg");

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("d53bda92-d23e-4c0d-bd8f-1b47a4e74c7f"),
                column: "ImageUrl",
                value: "/img/pickuptruck-img.jpg");

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("e2dc1b1c-cc60-4058-8e12-03f06c5a3688"),
                column: "ImageUrl",
                value: "/img/convertible-img.jpg");

            migrationBuilder.UpdateData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("f8c4b74f-1b2e-4714-b61a-8e1bcff0f008"),
                column: "ImageUrl",
                value: "/img/suv-img.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "VehicleTypes");
        }
    }
}
