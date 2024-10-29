using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models;

namespace RentACar.Data.Configurations
{
    public class VehicleTypeConfiguration : IEntityTypeConfiguration<VehicleType>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VehicleType> builder)
        {
            builder.HasData(SeedVehicleTypes());
        }

        private ICollection<VehicleType> SeedVehicleTypes()
        {
            List<VehicleType> vehicleTypes = new List<VehicleType>()
            {
                  new VehicleType
                  {
                      Id = new Guid("4b8bfe96-e6b2-41e6-b14c-bf2e5d3a49a0"),
                      Name = "Sedan",
                      Description = "A sedan is a passenger car in a three-box configuration with separate compartments forengine, passenger, and cargo."
                  },
                  new VehicleType
                  {
                      Id = new Guid("6f1b99c8-749f-4cf7-a30c-5d576b5098e6"),
                      Name = "Hatchback",
                      Description = "A hatchback is a car design featuring a rear door that swings upwards and typically has ashared volume for the passenger and cargo areas."
                  },
                  new VehicleType
                  {
                      Id = new Guid("83e0b3af-1f02-4b68-8e3b-87d469243024"),
                      Name = "Coupe",
                      Description = "A coupe is a car with a fixed roof and a sporty appearance, typically with two doors."
                  },
                  new VehicleType
                  {
                      Id = new Guid("f8c4b74f-1b2e-4714-b61a-8e1bcff0f008"),
                      Name = "SUV",
                      Description = "An SUV, or sport utility vehicle, combines elements of road-going passenger cars with offroad vehicles."
                  },
                  new VehicleType
                  {
                      Id = new Guid("2f60f64b-3c93-466f-b0f1-7b4005b5c75f"),
                      Name = "Sportscar",
                      Description = "A sports car is designed for high speed and performance, typically featuring a low bodyand powerful engine."
                  },
                  new VehicleType
                  {
                      Id = new Guid("e2dc1b1c-cc60-4058-8e12-03f06c5a3688"),
                      Name = "Convertible",
                      Description = "A convertible is a car with a flexible roof that can be either fully or partially opened."
                  },
                  new VehicleType
                  {
                      Id = new Guid("7b9c1cc8-0f3a-4b9f-8517-1b798e63f8b1"),
                      Name = "Crossover",
                      Description = "A crossover is a vehicle that combines features of a passenger vehicle with those of asport utility vehicle."
                  },
                  new VehicleType
                  {
                      Id = new Guid("97e1e204-7990-4c76-9b9f-4d60e5e2a44f"),
                      Name = "Muscle Car",
                      Description = "A muscle car is a high-performance vehicle that emphasizes power and speed, often with alarger engine."
                  },
                  new VehicleType
                  {
                      Id = new Guid("5c6c3d85-4a4c-4b9f-a0b4-fd6d6b21a38f"),
                      Name = "Station Wagon",
                      Description = "A station wagon is a car with an extended body and a hatch door at the back, offering mor cargo space."
                  },
                  new VehicleType
                  {
                      Id = new Guid("d53bda92-d23e-4c0d-bd8f-1b47a4e74c7f"),
                      Name = "Pickup Truck",
                      Description = "A pickup truck features an open cargo area with low sides and a tailgate, suitable fortransporting goods."
                  },
                  new VehicleType
                  {
                      Id = new Guid("8f5b0e36-d27a-4cf2-a22d-06c24094c62a"),
                      Name = "Jeep",
                      Description = "A Jeep is a rugged vehicle often designed for off-road use, characterized by a high groun clearance."
                  },
                  new VehicleType
                  {
                      Id = new Guid("bbab29d3-b20a-42c8-b7b8-0b012f2d186b"),
                      Name = "Limousine",
                      Description = "A limousine is a large, luxurious vehicle, often associated with high-end transport."
                  }
            };

            return vehicleTypes;
        }
    }
}
