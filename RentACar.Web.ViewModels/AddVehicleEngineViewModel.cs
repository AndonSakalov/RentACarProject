using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels
{
	public class AddVehicleEngineViewModel
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		public string FuelType { get; set; } = null!;

		[Required]
		public int HP { get; set; }

		[Required]
		public decimal Displacement { get; set; }

		[Required]
		public int CylindersCount { get; set; }

		public string DisplayName => FuelType == "Electric"
			? $"{FuelType} engine - {HP} HP"
			: $"{Displacement}L {CylindersCount}-cylinders {FuelType} engine - {HP} HP";
	}
}
