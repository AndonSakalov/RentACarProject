using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentACar.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "Address", "City", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("a7f6e5d4-c8b7-9012-faec-6789012345f0"), "9 Tsar Simeon Blvd", "Stara Zagora", "Bulgaria", "Stara Zagora" },
                    { new Guid("b8a7f6e5-d9c8-0123-0af1-7890123456a1"), "2 Tsarevets Str", "Veliko Tarnovo", "Bulgaria", "Veliko Tarnovo Old Town" },
                    { new Guid("c3d2e1f0-a4b5-6789-bcad-2345678901bc"), "42 Maritsa Blvd", "Plovdiv", "Bulgaria", "Plovdiv Downtown" },
                    { new Guid("c9b8a7f6-e0d9-1234-1ba2-8901234567b2"), "11 Bulgaria Blvd", "Pleven", "Bulgaria", "Pleven Central" },
                    { new Guid("d4c3b2a1-f5e6-7890-cbad-3456789012cd"), "15 Primorski Blvd", "Varna", "Bulgaria", "Varna Sea Garden" },
                    { new Guid("e5f4c3b2-a6d5-7890-dabc-4567890123de"), "1 Aeroport Street", "Burgas", "Bulgaria", "Burgas Airport" },
                    { new Guid("f0e1d2c3-b4a5-6789-abcd-1234567890ab"), "1 Vitosha Blvd", "Sofia", "Bulgaria", "Sofia Central" },
                    { new Guid("f6e5d4c3-b7a6-8901-ecbd-5678901234ef"), "24 Svoboda Square", "Ruse", "Bulgaria", "Ruse Center" }
                });

            migrationBuilder.InsertData(
                table: "Engines",
                columns: new[] { "Id", "CylindersCount", "Description", "Displacement", "FuelEfficiency", "FuelType", "HP", "IsElectric", "Torque" },
                values: new object[,]
                {
                    { new Guid("0c5f3e2f-a1b3-4f6e-a7f8-b2d5c8e4a2c9"), 8, "4.0L V8 high-performance gasoline engine.", 4.0m, 12.5m, 0, 450, false, 500 },
                    { new Guid("1d3c0f7f-8b2b-4a7c-8c5d-3b9b0d3f1e7a"), 4, "2.0L 4-cylinder gasoline engine with moderate power.", 2.0m, 8.5m, 0, 150, false, 200 },
                    { new Guid("3d2b1f6e-7e4c-8d3f-b2d5-a5f8c6e3b4c7"), 4, "1.8L hybrid engine with fuel efficiency for city driving.", 1.8m, 5.0m, 3, 200, false, 300 },
                    { new Guid("a2c93a4c-7e2b-4d3a-92d9-9e5b2b2d5c8f"), 4, "2.5L diesel engine, suitable for high-torque applications.", 2.5m, 6.0m, 1, 180, false, 400 },
                    { new Guid("e62d62f1-d2b2-4e4d-b5f2-8d4d3c6f1e5b"), 0, "Electric engine with high torque output.", 0.0m, 0.0m, 2, 300, true, 600 }
                });

            migrationBuilder.InsertData(
                table: "Makes",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("0c5f3e2f-a1b3-4f6e-a7f8-b2d5c8e4a2c9"), "South Korea", "Hyundai" },
                    { new Guid("1d3c0f7f-8b2b-4a7c-8c5d-3b9b0d3f1e7a"), "Japan", "Toyota" },
                    { new Guid("3d2b1f6e-7e4c-8d3f-b2d5-a5f8c6e3b4c7"), "France", "Renault" },
                    { new Guid("a1c7b2d3-f8e6-4d5b-7c3a-8f6e4b1c5d2b"), "Germany", "Audi" },
                    { new Guid("a2c93a4c-7e2b-4d3a-92d9-9e5b2b2d5c8f"), "United States", "Ford" },
                    { new Guid("b2d8f3a6-c7e1-4d5b-8a9f-e6f3d2c7b5a1"), "Italy", "Lamborghini" },
                    { new Guid("d5b1c6e3-a7f8-4d2b-b9f3-e1c5a2f6d8b3"), "Germany", "Porsche" },
                    { new Guid("e3d6f2c1-7b2f-5a4c-b8d6-a7c9f3e5d1b2"), "Germany", "Mercedes-Benz" },
                    { new Guid("e62d62f1-d2b2-4e4d-b5f2-8d4d3c6f1e5b"), "Germany", "BMW" },
                    { new Guid("f1b3d7e4-a2c6-8f5b-7e4d-d3f2c5b1a6f7"), "Sweden", "Volvo" }
                });

            migrationBuilder.InsertData(
                table: "Transmissions",
                columns: new[] { "Id", "GearsCount", "Type" },
                values: new object[,]
                {
                    { new Guid("a60b3780-f473-4e25-8e5c-04e4f5572db8"), 7, 1 },
                    { new Guid("b44f8362-d0c7-4a69-a72b-9073b09f4a54"), 6, 0 },
                    { new Guid("b5f1426e-1b8d-4459-9b8e-2985db88f48e"), 9, 1 },
                    { new Guid("dc7eabfa-3e7f-4cc3-b4ef-e0f2168749c4"), 8, 1 },
                    { new Guid("f7c0d5b2-98c3-4a75-85d8-8d0c36b22929"), 5, 0 }
                });

            migrationBuilder.InsertData(
                table: "VehicleTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("2f60f64b-3c93-466f-b0f1-7b4005b5c75f"), "A sports car is designed for high speed and performance, typically featuring a low bodyand powerful engine.", "Sportscar" },
                    { new Guid("4b8bfe96-e6b2-41e6-b14c-bf2e5d3a49a0"), "A sedan is a passenger car in a three-box configuration with separate compartments forengine, passenger, and cargo.", "Sedan" },
                    { new Guid("5c6c3d85-4a4c-4b9f-a0b4-fd6d6b21a38f"), "A station wagon is a car with an extended body and a hatch door at the back, offering mor cargo space.", "Station Wagon" },
                    { new Guid("6f1b99c8-749f-4cf7-a30c-5d576b5098e6"), "A hatchback is a car design featuring a rear door that swings upwards and typically has ashared volume for the passenger and cargo areas.", "Hatchback" },
                    { new Guid("7b9c1cc8-0f3a-4b9f-8517-1b798e63f8b1"), "A crossover is a vehicle that combines features of a passenger vehicle with those of asport utility vehicle.", "Crossover" },
                    { new Guid("83e0b3af-1f02-4b68-8e3b-87d469243024"), "A coupe is a car with a fixed roof and a sporty appearance, typically with two doors.", "Coupe" },
                    { new Guid("8f5b0e36-d27a-4cf2-a22d-06c24094c62a"), "A Jeep is a rugged vehicle often designed for off-road use, characterized by a high groun clearance.", "Jeep" },
                    { new Guid("97e1e204-7990-4c76-9b9f-4d60e5e2a44f"), "A muscle car is a high-performance vehicle that emphasizes power and speed, often with alarger engine.", "Muscle Car" },
                    { new Guid("bbab29d3-b20a-42c8-b7b8-0b012f2d186b"), "A limousine is a large, luxurious vehicle, often associated with high-end transport.", "Limousine" },
                    { new Guid("d53bda92-d23e-4c0d-bd8f-1b47a4e74c7f"), "A pickup truck features an open cargo area with low sides and a tailgate, suitable fortransporting goods.", "Pickup Truck" },
                    { new Guid("e2dc1b1c-cc60-4058-8e12-03f06c5a3688"), "A convertible is a car with a flexible roof that can be either fully or partially opened.", "Convertible" },
                    { new Guid("f8c4b74f-1b2e-4714-b61a-8e1bcff0f008"), "An SUV, or sport utility vehicle, combines elements of road-going passenger cars with offroad vehicles.", "SUV" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "AddedOn", "BranchId", "Color", "DoorsCount", "EngineId", "ImageUrl", "MakeId", "Mileage", "Model", "PricePerDay", "RegistrationNumber", "RentalId", "SeatsCount", "TransmissionId", "VINNumber", "VehicleTypeId", "Year" },
                values: new object[,]
                {
                    { new Guid("8a6fbd63-0c0e-4b82-a5c5-c50f9de6ec12"), new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0e1d2c3-b4a5-6789-abcd-1234567890ab"), "Red", 4, new Guid("3d2b1f6e-7e4c-8d3f-b2d5-a5f8c6e3b4c7"), "/img/toyota-camry-red.jfif", new Guid("1d3c0f7f-8b2b-4a7c-8c5d-3b9b0d3f1e7a"), 15000, "Camry", 50.00m, "ABC123", null, 5, new Guid("f7c0d5b2-98c3-4a75-85d8-8d0c36b22929"), "1HGBH41JXMN109186", new Guid("4b8bfe96-e6b2-41e6-b14c-bf2e5d3a49a0"), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c59b3736-645b-471b-9c0b-205792c82b8e"), new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0e1d2c3-b4a5-6789-abcd-1234567890ab"), "White", 4, new Guid("3d2b1f6e-7e4c-8d3f-b2d5-a5f8c6e3b4c7"), "/img/Toyota-Corolla-white.jpg", new Guid("1d3c0f7f-8b2b-4a7c-8c5d-3b9b0d3f1e7a"), 5000, "Corolla", 45.00m, "JKL012", null, 5, new Guid("dc7eabfa-3e7f-4cc3-b4ef-e0f2168749c4"), "5YFBURHE8JP123456", new Guid("4b8bfe96-e6b2-41e6-b14c-bf2e5d3a49a0"), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d9e0f547-3b8e-42a6-a8a5-6c74ec9b0154"), new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0e1d2c3-b4a5-6789-abcd-1234567890ab"), "Grey", 4, new Guid("0c5f3e2f-a1b3-4f6e-a7f8-b2d5c8e4a2c9"), "/img/Ford-F150-grey.jfif", new Guid("a2c93a4c-7e2b-4d3a-92d9-9e5b2b2d5c8f"), 20000, "F-150", 75.00m, "XYZ789", null, 5, new Guid("dc7eabfa-3e7f-4cc3-b4ef-e0f2168749c4"), "2FTRX18L8XCA12345", new Guid("d53bda92-d23e-4c0d-bd8f-1b47a4e74c7f"), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e6c4c94a-8c92-44f4-a213-7bdbf3e5a57f"), new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0e1d2c3-b4a5-6789-abcd-1234567890ab"), "Black", 4, new Guid("a2c93a4c-7e2b-4d3a-92d9-9e5b2b2d5c8f"), "/img/BMW-X5-black.jpg", new Guid("e62d62f1-d2b2-4e4d-b5f2-8d4d3c6f1e5b"), 25000, "X5", 100.00m, "LMN456", null, 5, new Guid("b44f8362-d0c7-4a69-a72b-9073b09f4a54"), "3MZBN1V75AM108070", new Guid("f8c4b74f-1b2e-4714-b61a-8e1bcff0f008"), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("a7f6e5d4-c8b7-9012-faec-6789012345f0"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("b8a7f6e5-d9c8-0123-0af1-7890123456a1"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("c3d2e1f0-a4b5-6789-bcad-2345678901bc"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("c9b8a7f6-e0d9-1234-1ba2-8901234567b2"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("d4c3b2a1-f5e6-7890-cbad-3456789012cd"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("e5f4c3b2-a6d5-7890-dabc-4567890123de"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("f6e5d4c3-b7a6-8901-ecbd-5678901234ef"));

            migrationBuilder.DeleteData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: new Guid("1d3c0f7f-8b2b-4a7c-8c5d-3b9b0d3f1e7a"));

            migrationBuilder.DeleteData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: new Guid("e62d62f1-d2b2-4e4d-b5f2-8d4d3c6f1e5b"));

            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "Id",
                keyValue: new Guid("0c5f3e2f-a1b3-4f6e-a7f8-b2d5c8e4a2c9"));

            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "Id",
                keyValue: new Guid("3d2b1f6e-7e4c-8d3f-b2d5-a5f8c6e3b4c7"));

            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "Id",
                keyValue: new Guid("a1c7b2d3-f8e6-4d5b-7c3a-8f6e4b1c5d2b"));

            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "Id",
                keyValue: new Guid("b2d8f3a6-c7e1-4d5b-8a9f-e6f3d2c7b5a1"));

            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "Id",
                keyValue: new Guid("d5b1c6e3-a7f8-4d2b-b9f3-e1c5a2f6d8b3"));

            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "Id",
                keyValue: new Guid("e3d6f2c1-7b2f-5a4c-b8d6-a7c9f3e5d1b2"));

            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "Id",
                keyValue: new Guid("f1b3d7e4-a2c6-8f5b-7e4d-d3f2c5b1a6f7"));

            migrationBuilder.DeleteData(
                table: "Transmissions",
                keyColumn: "Id",
                keyValue: new Guid("a60b3780-f473-4e25-8e5c-04e4f5572db8"));

            migrationBuilder.DeleteData(
                table: "Transmissions",
                keyColumn: "Id",
                keyValue: new Guid("b5f1426e-1b8d-4459-9b8e-2985db88f48e"));

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("2f60f64b-3c93-466f-b0f1-7b4005b5c75f"));

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("5c6c3d85-4a4c-4b9f-a0b4-fd6d6b21a38f"));

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("6f1b99c8-749f-4cf7-a30c-5d576b5098e6"));

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("7b9c1cc8-0f3a-4b9f-8517-1b798e63f8b1"));

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("83e0b3af-1f02-4b68-8e3b-87d469243024"));

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("8f5b0e36-d27a-4cf2-a22d-06c24094c62a"));

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("97e1e204-7990-4c76-9b9f-4d60e5e2a44f"));

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("bbab29d3-b20a-42c8-b7b8-0b012f2d186b"));

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("e2dc1b1c-cc60-4058-8e12-03f06c5a3688"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("8a6fbd63-0c0e-4b82-a5c5-c50f9de6ec12"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("c59b3736-645b-471b-9c0b-205792c82b8e"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("d9e0f547-3b8e-42a6-a8a5-6c74ec9b0154"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("e6c4c94a-8c92-44f4-a213-7bdbf3e5a57f"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("f0e1d2c3-b4a5-6789-abcd-1234567890ab"));

            migrationBuilder.DeleteData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: new Guid("0c5f3e2f-a1b3-4f6e-a7f8-b2d5c8e4a2c9"));

            migrationBuilder.DeleteData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: new Guid("3d2b1f6e-7e4c-8d3f-b2d5-a5f8c6e3b4c7"));

            migrationBuilder.DeleteData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: new Guid("a2c93a4c-7e2b-4d3a-92d9-9e5b2b2d5c8f"));

            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "Id",
                keyValue: new Guid("1d3c0f7f-8b2b-4a7c-8c5d-3b9b0d3f1e7a"));

            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "Id",
                keyValue: new Guid("a2c93a4c-7e2b-4d3a-92d9-9e5b2b2d5c8f"));

            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "Id",
                keyValue: new Guid("e62d62f1-d2b2-4e4d-b5f2-8d4d3c6f1e5b"));

            migrationBuilder.DeleteData(
                table: "Transmissions",
                keyColumn: "Id",
                keyValue: new Guid("b44f8362-d0c7-4a69-a72b-9073b09f4a54"));

            migrationBuilder.DeleteData(
                table: "Transmissions",
                keyColumn: "Id",
                keyValue: new Guid("dc7eabfa-3e7f-4cc3-b4ef-e0f2168749c4"));

            migrationBuilder.DeleteData(
                table: "Transmissions",
                keyColumn: "Id",
                keyValue: new Guid("f7c0d5b2-98c3-4a75-85d8-8d0c36b22929"));

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("4b8bfe96-e6b2-41e6-b14c-bf2e5d3a49a0"));

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("d53bda92-d23e-4c0d-bd8f-1b47a4e74c7f"));

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("f8c4b74f-1b2e-4714-b61a-8e1bcff0f008"));
        }
    }
}
