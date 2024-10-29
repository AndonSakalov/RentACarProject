using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Data.Models;
using RentACar.Data.Models.Enums;

namespace RentACar.Data.Configurations
{
    public class TransmissionConfiguration : IEntityTypeConfiguration<Transmission>
    {
        public void Configure(EntityTypeBuilder<Transmission> builder)
        {
            builder.HasData(SeedTransmissions());
        }

        private ICollection<Transmission> SeedTransmissions()
        {
            List<Transmission> transmissions = new List<Transmission>()
            {
                new Transmission
                {
                    Id = new Guid("f7c0d5b2-98c3-4a75-85d8-8d0c36b22929"),
                    GearsCount = 5,
                    Type = TransmissionType.Manual
                },
                new Transmission
                {
                    Id = new Guid("b44f8362-d0c7-4a69-a72b-9073b09f4a54"),
                    GearsCount = 6,
                    Type = TransmissionType.Manual
                },
                new Transmission
                {
                    Id = new Guid("dc7eabfa-3e7f-4cc3-b4ef-e0f2168749c4"),
                    GearsCount = 8,
                    Type = TransmissionType.Automatic
                },
                new Transmission
                {
                    Id = new Guid("a60b3780-f473-4e25-8e5c-04e4f5572db8"),
                    GearsCount = 7,
                    Type = TransmissionType.Automatic
                },
                new Transmission
                {
                    Id = new Guid("b5f1426e-1b8d-4459-9b8e-2985db88f48e"),
                    GearsCount = 9,
                    Type = TransmissionType.Automatic
                }
            };

            return transmissions;
        }
    }
}
