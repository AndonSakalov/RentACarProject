using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels.Rental
{
	public class CustomerViewModel
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		public string UserName { get; set; } = null!;

		[Required]
		public string Email { get; set; } = null!;

		public string? PhoneNumber { get; set; }
	}
}
