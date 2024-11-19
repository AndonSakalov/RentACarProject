using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels
{
	public class SearchUserViewModel
	{
		[Required]
		public string Email { get; set; } = null!;

		public string? Username { get; set; }

		public bool IsFound { get; set; }

		public Guid UserId { get; set; }
	}
}
