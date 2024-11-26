using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels
{
	public class EditVehicleListViewModel
	{
		[Required]
		public Guid BranchId { get; set; }

		[Required]
		public string Make { get; set; } = null!;

		[Required]
		public string Model { get; set; } = null!;

		[Required]
		public int SeatsCount { get; set; }

		[Required]
		public string TransmissionType { get; set; } = null!;

		[Required]
		public decimal Mileage { get; set; }

		[Required]
		public string ImageUrl { get; set; } = null!;

		[Required]
		public string VehicleType { get; set; } = null!;

		[Required]
		public string BranchName { get; set; } = null!;
	}
}
