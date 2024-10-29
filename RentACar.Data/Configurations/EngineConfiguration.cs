using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Data.Models;
using RentACar.Data.Models.Enums;

namespace RentACar.Data.Configurations
{
    public class EngineConfiguration : IEntityTypeConfiguration<Engine>
    {
        public void Configure(EntityTypeBuilder<Engine> builder)
        {
            builder.HasData(SeedEngines());
        }

        private ICollection<Engine> SeedEngines()
        {
            List<Engine> engines = new List<Engine>()
            {
                new Engine
                {
                    Id = new Guid("1d3c0f7f-8b2b-4a7c-8c5d-3b9b0d3f1e7a"),
                    FuelType = FuelType.Petrol,
                    HP = 150,
                    Torque = 200,
                    FuelEfficiency = 8.5m,
                    Displacement = 2.0m,
                    CylindersCount = 4,
                    Description = "2.0L 4-cylinder gasoline engine with moderate power.",
                    IsElectric = false
                },
                new Engine
                {
                    Id = new Guid("a2c93a4c-7e2b-4d3a-92d9-9e5b2b2d5c8f"),
                    FuelType = FuelType.Diesel,
                    HP = 180,
                    Torque = 400,
                    FuelEfficiency = 6.0m,
                    Displacement = 2.5m,
                    CylindersCount = 4,
                    Description = "2.5L diesel engine, suitable for high-torque applications.",
                    IsElectric = false
                },
                new Engine
                {
                    Id = new Guid("e62d62f1-d2b2-4e4d-b5f2-8d4d3c6f1e5b"),
                    FuelType = FuelType.Electric,
                    HP = 300,
                    Torque = 600,
                    FuelEfficiency = 0.0m, // Typically 0 for electric engines as they don't burn fuel
                    Displacement = 0.0m,
                    CylindersCount = 0,
                    Description = "Electric engine with high torque output.",
                    IsElectric = true
                },
                new Engine
                {
                    Id = new Guid("0c5f3e2f-a1b3-4f6e-a7f8-b2d5c8e4a2c9"),
                    FuelType = FuelType.Petrol,
                    HP = 450,
                    Torque = 500,
                    FuelEfficiency = 12.5m,
                    Displacement = 4.0m,
                    CylindersCount = 8,
                    Description = "4.0L V8 high-performance gasoline engine.",
                    IsElectric = false
                },
                new Engine
                {
                    Id = new Guid("3d2b1f6e-7e4c-8d3f-b2d5-a5f8c6e3b4c7"),
                    FuelType = FuelType.Hybrid,
                    HP = 200,
                    Torque = 300,
                    FuelEfficiency = 5.0m,
                    Displacement = 1.8m,
                    CylindersCount = 4,
                    Description = "1.8L hybrid engine with fuel efficiency for city driving.",
                    IsElectric = false
                }
        };

            return engines;
        }
    }
}

