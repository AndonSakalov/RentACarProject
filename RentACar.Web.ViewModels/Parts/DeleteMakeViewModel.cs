using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels.Parts
{
	public class DeleteMakeViewModel
	{
		public Guid Id { get; set; }

		[Required]
		public string Name { get; set; } = null!;

		[Required]
		public string Country { get; set; } = null!;
	}
}
