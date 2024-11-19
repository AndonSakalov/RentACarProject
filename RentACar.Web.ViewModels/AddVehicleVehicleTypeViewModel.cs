using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels
{
	public class AddVehicleVehicleTypeViewModel
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		public string Name { get; set; } = null!;
	}
}
