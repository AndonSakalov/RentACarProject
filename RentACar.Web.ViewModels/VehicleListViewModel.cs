using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels
{
	public class VehicleListViewModel
	{
		[Required]
		public string Name { get; set; } = null!;

		[Required]
		public int SeatsCount { get; set; }

		[Required]
		public string TransmissionType { get; set; } = null!;

		[Required]
		public decimal Mileage { get; set; }

		[Required]
		public decimal PriceForSelectedDays { get; set; }

		[Required]
		public int RentalLengthInDays { get; set; }

		[Required]
		public string ImageUrl { get; set; } = null!;

		[Required]
		public string VehicleType { get; set; } = null!;

		[Required]
		public string BranchName { get; set; } = null!;
	}
}
