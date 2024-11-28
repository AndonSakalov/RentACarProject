using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static RentACar.Common.EntityValidationConstants.Vehicle;

namespace RentACar.Web.ViewModels
{
	public class EditVehicleViewModel
	{
		[Required]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Color is required.")]
		[MinLength(ColorMinLength, ErrorMessage = "Color should be atleast 3 symbols.")]
		[MaxLength(ColorMaxLength, ErrorMessage = "Color can be up to 30 symbols.")]
		public string Color { get; set; } = null!;

		[Required]
		public Guid TransmissionId { get; set; }

		public List<AddVehicleTransmissionViewModel> Transmissions { get; set; } = new List<AddVehicleTransmissionViewModel>();

		[MinLength(RegistrationMinLength)]
		[MaxLength(RegistrationMaxLength)]
		public string? RegistrationNumber { get; set; }

		[Required]
		public string? ImageUrl { get; set; }

		[Required]
		public Guid BranchId { get; set; }
		public List<AddVehicleBranchViewModel> Branches { get; set; } = new List<AddVehicleBranchViewModel>();

		[Required]
		public Guid EngineId { get; set; }
		public List<AddVehicleEngineViewModel> Engines { get; set; } = new List<AddVehicleEngineViewModel>();

		[Required]
		[Range(PricePerDayMinValue, PricePerDayMaxValue)]
		[Precision(18, 2)]
		public decimal PricePerDay { get; set; }





	}
}
