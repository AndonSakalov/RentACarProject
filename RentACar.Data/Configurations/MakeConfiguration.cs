using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Data.Models;

namespace RentACar.Data.Configurations
{
    public class MakeConfiguration : IEntityTypeConfiguration<Make>
    {
        public void Configure(EntityTypeBuilder<Make> builder)
        {
            builder.Property(m => m.IsDeleted)
                .HasDefaultValue(false);

            builder.HasData(SeedData());
        }

        private ICollection<Make> SeedData()
        {
            List<Make> makes = new List<Make>()
            {
                new Make
                {
                    Id = new Guid("1d3c0f7f-8b2b-4a7c-8c5d-3b9b0d3f1e7a"),
                    Name = "Toyota",
                    Country = "Japan"
                },
                new Make
                {
                    Id = new Guid("a2c93a4c-7e2b-4d3a-92d9-9e5b2b2d5c8f"),
                    Name = "Ford",
                    Country = "United States"
                },
                new Make
                {
                    Id = new Guid("e62d62f1-d2b2-4e4d-b5f2-8d4d3c6f1e5b"),
                    Name = "BMW",
                    Country = "Germany"
                },
                new Make
                {
                    Id = new Guid("0c5f3e2f-a1b3-4f6e-a7f8-b2d5c8e4a2c9"),
                    Name = "Hyundai",
                    Country = "South Korea"
                },
                new Make
                {
                    Id = new Guid("3d2b1f6e-7e4c-8d3f-b2d5-a5f8c6e3b4c7"),
                    Name = "Renault",
                    Country = "France"
                },
                new Make
                {
                    Id = new Guid("f1b3d7e4-a2c6-8f5b-7e4d-d3f2c5b1a6f7"),
                    Name = "Volvo",
                    Country = "Sweden"
                },
                new Make
                {
                    Id = new Guid("a1c7b2d3-f8e6-4d5b-7c3a-8f6e4b1c5d2b"),
                    Name = "Audi",
                    Country = "Germany"
                },
                new Make
                {
                    Id = new Guid("e3d6f2c1-7b2f-5a4c-b8d6-a7c9f3e5d1b2"),
                    Name = "Mercedes-Benz",
                    Country = "Germany"
                },
                new Make
                {
                    Id = new Guid("b2d8f3a6-c7e1-4d5b-8a9f-e6f3d2c7b5a1"),
                    Name = "Lamborghini",
                    Country = "Italy"
                },
                new Make
                {
                    Id = new Guid("d5b1c6e3-a7f8-4d2b-b9f3-e1c5a2f6d8b3"),
                    Name = "Porsche",
                    Country = "Germany"
                }
            };

            return makes;
        }
    }
}
