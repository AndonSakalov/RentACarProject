using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels.Rental
{
	public class RentalVehicleViewModel
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		public string Name { get; set; } = null!;

		[Required]
		public int SeatsCount { get; set; }

		[Required]
		public string TransmissionType { get; set; } = null!;

		[Required]
		public string ImageUrl { get; set; } = null!;


	}
}
