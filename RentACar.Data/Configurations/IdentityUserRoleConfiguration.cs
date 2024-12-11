using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentACar.Data.Configurations
{
	public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
		{
			builder.HasData(
				new IdentityUserRole<Guid>
				{
					UserId = new Guid("F12A217A-ABEC-41F8-A8A9-7C97B35497C3"), // Admin User GUID
					RoleId = new Guid("5C8FA736-6CF6-43D8-8F9B-29A3585D620E")  // Admin Role GUID
				},
				// Assign Staff user to Staff role
				new IdentityUserRole<Guid>
				{
					UserId = new Guid("9A8F4F39-C727-48B4-8D73-DA47C1B35D92"), // Staff User GUID
					RoleId = new Guid("3E832D2C-2E76-4C94-BF8F-68E2D2A3F7D3")  // Staff Role GUID
				},
				// Assign Customer user to Customer role
				new IdentityUserRole<Guid>
				{
					UserId = new Guid("00C59882-2A85-429E-9A3E-B1A3F7D9F8D7"), // Customer User GUID
					RoleId = new Guid("8DADF6D7-CB91-4873-BE73-29F4C09BCB70")  // Customer Role GUID
				}
			);
		}
	}
}
