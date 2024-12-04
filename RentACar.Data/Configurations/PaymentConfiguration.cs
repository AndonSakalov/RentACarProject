using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Data.Models;
using RentACar.Data.Models.Enums;

namespace RentACar.Data.Configurations
{
	public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
	{
		public void Configure(EntityTypeBuilder<Payment> builder)
		{
			builder.Property(p => p.Amount)
				.HasDefaultValue(0.00);

			builder.Property(p => p.Status)
				.HasDefaultValue(PaymentStatus.Pending);

			builder.Property(p => p.PaymentMethod)
			.HasDefaultValue(PaymentMethod.Cash);
		}
	}
}
