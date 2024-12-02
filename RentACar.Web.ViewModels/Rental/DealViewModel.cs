using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels.Rental
{
	public class DealViewModel
	{
		[Required]
		public RentalBranchViewModel Branch { get; set; } = new RentalBranchViewModel();

		[Required]
		public RentalVehicleViewModel Vehicle { get; set; } = new RentalVehicleViewModel();

		[Required]
		public DateTime PickupDate { get; set; }

		[Required]
		public DateTime ReturnDate { get; set; }

		[Required]
		public decimal Price { get; set; }

		[Required]
		public string VehicleType { get; set; } = null!;
	}
}
