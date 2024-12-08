using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Data.Models;

namespace RentACar.Data.Configurations
{
	public class RentalConfiguration : IEntityTypeConfiguration<Rental>
	{
		public void Configure(EntityTypeBuilder<Rental> builder)
		{
			builder.Property(r => r.IsActive)
				.HasDefaultValue(true); // When a rental is created(instanced) the value of IsActive is set to true.

			builder.HasOne(r => r.Vehicle)
			   .WithOne(v => v.Rental)
			   .HasForeignKey<Rental>(r => r.Id)
			   .OnDelete(DeleteBehavior.NoAction);
		}
	}
}
