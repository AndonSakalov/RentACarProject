using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Data.Models;

namespace RentACar.Data.Configurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasData(SeedBranches());
        }

        private ICollection<Branch> SeedBranches()
        {
            List<Branch> branches = new List<Branch>()
           {
                new Branch
                {
                    Id = new Guid("f0e1d2c3-b4a5-6789-abcd-1234567890ab"),
                    Name = "Sofia Central",
                    Address = "1 Vitosha Blvd",
                    City = "Sofia",
                    Country = "Bulgaria"
                },
                new Branch
                {
                    Id = new Guid("c3d2e1f0-a4b5-6789-bcad-2345678901bc"),
                    Name = "Plovdiv Downtown",
                    Address = "42 Maritsa Blvd",
                    City = "Plovdiv",
                    Country = "Bulgaria"
                },
                new Branch
                {
                    Id = new Guid("d4c3b2a1-f5e6-7890-cbad-3456789012cd"),
                    Name = "Varna Sea Garden",
                    Address = "15 Primorski Blvd",
                    City = "Varna",
                    Country = "Bulgaria"
                },
                new Branch
                {
                    Id = new Guid("e5f4c3b2-a6d5-7890-dabc-4567890123de"),
                    Name = "Burgas Airport",
                    Address = "1 Aeroport Street",
                    City = "Burgas",
                    Country = "Bulgaria"
                },
                new Branch
                {
                    Id = new Guid("f6e5d4c3-b7a6-8901-ecbd-5678901234ef"),
                    Name = "Ruse Center",
                    Address = "24 Svoboda Square",
                    City = "Ruse",
                    Country = "Bulgaria"
                },
                new Branch
                {
                    Id = new Guid("a7f6e5d4-c8b7-9012-faec-6789012345f0"),
                    Name = "Stara Zagora",
                    Address = "9 Tsar Simeon Blvd",
                    City = "Stara Zagora",
                    Country = "Bulgaria"
                },
                new Branch
                {
                    Id = new Guid("b8a7f6e5-d9c8-0123-0af1-7890123456a1"),
                    Name = "Veliko Tarnovo Old Town",
                    Address = "2 Tsarevets Str",
                    City = "Veliko Tarnovo",
                    Country = "Bulgaria"
                },
                new Branch
                {
                    Id = new Guid("c9b8a7f6-e0d9-1234-1ba2-8901234567b2"),
                    Name = "Pleven Central",
                    Address = "11 Bulgaria Blvd",
                    City = "Pleven",
                    Country = "Bulgaria"
                }
           };

            return branches;
        }
    }
}
