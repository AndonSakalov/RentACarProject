using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels
{
	public class ServiceVehicleViewModel
	{
		[Required]
		public Guid VehicleId { get; set; }

		[Required]
		public string Make { get; set; } = null!;

		[Required]
		public string Model { get; set; } = null!;

		[Required]
		public int ServicedAt { get; set; }

		[Required]
		public int Mileage { get; set; }

		[Required]
		public string ImageUrl { get; set; } = null!;
	}
}
