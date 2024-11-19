using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels
{
	public class AddVehicleTransmissionViewModel
	{
		[Required]
		public Guid Id { get; set; }
		[Required]
		public string Type { get; set; } = null!;
		[Required]
		public int GearsCount { get; set; }
		public string DisplayName => $"{Type} ({GearsCount} Gears)";
	}
}
