using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Data.Models;
using static RentACar.Common.EntityValidationConstants.Vehicle;

namespace RentACar.Data.Configurations
{
	public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
	{
		public void Configure(EntityTypeBuilder<Vehicle> builder)
		{
			builder.Property(v => v.IsDeleted)
				.HasDefaultValue(false);

			builder.Property(v => v.ImageUrl)
				.HasDefaultValue(NoImageUrl);

			builder.Property(v => v.AddedOn)
				.ValueGeneratedOnAdd();

			builder.HasData(SeedVehicles());

			builder.Property(v => v.IsRented)
				.HasDefaultValue(false);

			builder.HasOne(v => v.Rental)
		   .WithOne(r => r.Vehicle)
		   .HasForeignKey<Vehicle>(v => v.RentalId)
		   .OnDelete(DeleteBehavior.SetNull);
		}

		private ICollection<Vehicle> SeedVehicles()
		{

			DateTime fixedDate = new DateTime(2024, 10, 25);

			List<Vehicle> vehicles = new List<Vehicle>()
			{
				new Vehicle
				{
					Id = new Guid("8a6fbd63-0c0e-4b82-a5c5-c50f9de6ec12"),
					MakeId = new Guid("1d3c0f7f-8b2b-4a7c-8c5d-3b9b0d3f1e7a"),
					Color = "Red",
					Model = "Camry",
					VehicleTypeId = new Guid("4b8bfe96-e6b2-41e6-b14c-bf2e5d3a49a0"),
					TransmissionId = new Guid("f7c0d5b2-98c3-4a75-85d8-8d0c36b22929"),
					SeatsCount = 5,
					DoorsCount = 4,
					Year = 2022,
					Mileage = 15000,
					RegistrationNumber = "ABC123",
					ImageUrl = "/img/toyota-camry-red.jfif",
					VINNumber = "1HGBH41JXMN109186",
					AddedOn = fixedDate,
					BranchId = new Guid("f0e1d2c3-b4a5-6789-abcd-1234567890ab"),
					EngineId = new Guid("3d2b1f6e-7e4c-8d3f-b2d5-a5f8c6e3b4c7"),
					PricePerDay = 50.00m
				},
				new Vehicle
				{
					Id = new Guid("d9e0f547-3b8e-42a6-a8a5-6c74ec9b0154"),
					MakeId = new Guid("a2c93a4c-7e2b-4d3a-92d9-9e5b2b2d5c8f"),
					Color = "Grey",
					Model = "F-150",
					VehicleTypeId = new Guid("d53bda92-d23e-4c0d-bd8f-1b47a4e74c7f"),
					TransmissionId = new Guid("dc7eabfa-3e7f-4cc3-b4ef-e0f2168749c4"),
					SeatsCount = 5,
					DoorsCount = 4,
					Year = 2021,
					Mileage = 20000,
					RegistrationNumber = "XYZ789",
					ImageUrl = "/img/Ford-F150-grey.jfif",
					VINNumber = "2FTRX18L8XCA12345",
					AddedOn = fixedDate,
					BranchId = new Guid("f0e1d2c3-b4a5-6789-abcd-1234567890ab"),
					EngineId = new Guid("0c5f3e2f-a1b3-4f6e-a7f8-b2d5c8e4a2c9"),
					PricePerDay = 75.00m
				},
				new Vehicle
				{
					Id = new Guid("e6c4c94a-8c92-44f4-a213-7bdbf3e5a57f"),
					MakeId = new Guid("e62d62f1-d2b2-4e4d-b5f2-8d4d3c6f1e5b"),
					Color = "Black",
					Model = "X5",
					VehicleTypeId = new Guid("f8c4b74f-1b2e-4714-b61a-8e1bcff0f008"),
					TransmissionId = new Guid("b44f8362-d0c7-4a69-a72b-9073b09f4a54"),
					SeatsCount = 5,
					DoorsCount = 4,
					Year = 2020,
					Mileage = 25000,
					RegistrationNumber = "LMN456",
					ImageUrl = "/img/BMW-X5-black.jpg",
					VINNumber = "3MZBN1V75AM108070",
					AddedOn = fixedDate,
					BranchId = new Guid("f0e1d2c3-b4a5-6789-abcd-1234567890ab"),
					EngineId = new Guid("a2c93a4c-7e2b-4d3a-92d9-9e5b2b2d5c8f"),
					PricePerDay = 100.00m
				},
				new Vehicle
				{
					Id = new Guid("c59b3736-645b-471b-9c0b-205792c82b8e"),
					MakeId = new Guid("1d3c0f7f-8b2b-4a7c-8c5d-3b9b0d3f1e7a"),
					Color = "White",
					Model = "Corolla",
					VehicleTypeId = new Guid("4b8bfe96-e6b2-41e6-b14c-bf2e5d3a49a0"),
					TransmissionId = new Guid("dc7eabfa-3e7f-4cc3-b4ef-e0f2168749c4"),
					SeatsCount = 5,
					DoorsCount = 4,
					Year = 2022,
					Mileage = 5000,
					RegistrationNumber = "JKL012",
					ImageUrl = "/img/Toyota-Corolla-white.jpg",
					VINNumber = "5YFBURHE8JP123456",
					AddedOn = fixedDate,
					BranchId = new Guid("f0e1d2c3-b4a5-6789-abcd-1234567890ab"),
					EngineId = new Guid("3d2b1f6e-7e4c-8d3f-b2d5-a5f8c6e3b4c7"),
					PricePerDay = 45.00m
				},
			};

			return vehicles;
		}
	}
}
