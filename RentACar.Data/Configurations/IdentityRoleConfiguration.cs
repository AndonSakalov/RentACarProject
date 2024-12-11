using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentACar.Data.Configurations
{
	public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
	{

		public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
		{
			builder.HasData(
			   new IdentityRole<Guid>
			   {
				   Id = new Guid("8DADF6D7-CB91-4873-BE73-29F4C09BCB70"),
				   Name = "Customer",
				   NormalizedName = "CUSTOMER"
			   },
			   new IdentityRole<Guid>
			   {
				   Id = new Guid("5C8FA736-6CF6-43D8-8F9B-29A3585D620E"),
				   Name = "Admin",
				   NormalizedName = "ADMIN"
			   },
			   new IdentityRole<Guid>
			   {
				   Id = new Guid("3E832D2C-2E76-4C94-BF8F-68E2D2A3F7D3"),
				   Name = "Staff",
				   NormalizedName = "STAFF"
			   }
		   );
		}
	}
}
