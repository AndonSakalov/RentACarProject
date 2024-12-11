using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Data.Models;

namespace RentACar.Data.Configurations
{
	public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.
				HasIndex(au => au.Email)
				.IsUnique();

			builder.HasData(SeedUsers());
		}
		private ICollection<ApplicationUser> SeedUsers()
		{
			var passwordHasher = new PasswordHasher<ApplicationUser>();
			List<ApplicationUser> applicationUsers = new List<ApplicationUser>();

			var admin = new ApplicationUser()
			{
				Id = new Guid("F12A217A-ABEC-41F8-A8A9-7C97B35497C3"),
				Email = "admin@example.com",
				UserName = "AdminUser",
				NormalizedEmail = "ADMIN@EXAMPLE.COM",
				NormalizedUserName = "ADMINUSER",
				SecurityStamp = Guid.NewGuid().ToString()
			};
			admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin123*");

			var staff = new ApplicationUser
			{
				Id = new Guid("9A8F4F39-C727-48B4-8D73-DA47C1B35D92"),
				Email = "staff@example.com",
				UserName = "StaffUser",
				NormalizedEmail = "STAFF@EXAMPLE.COM",
				NormalizedUserName = "STAFFUSER",
				SecurityStamp = Guid.NewGuid().ToString()
			};
			staff.PasswordHash = passwordHasher.HashPassword(staff, "Staff123*");

			var customer = new ApplicationUser
			{
				Id = new Guid("00C59882-2A85-429E-9A3E-B1A3F7D9F8D7"),
				Email = "customer@example.com",
				UserName = "CustomerUser",
				NormalizedEmail = "CUSTOMER@EXAMPLE.COM",
				NormalizedUserName = "CUSTOMERUSER",
				SecurityStamp = Guid.NewGuid().ToString()
			};
			customer.PasswordHash = passwordHasher.HashPassword(customer, "Customer123*");

			applicationUsers.Add(admin);
			applicationUsers.Add(staff);
			applicationUsers.Add(customer);

			return applicationUsers;
		}
	}
}
