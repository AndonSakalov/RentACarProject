using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static RentACar.Common.EntityValidationConstants.Vehicle;
using static RentACar.Common.EntityValidationErrorMessages.Vehicle;
namespace RentACar.Web.ViewModels
{
	public class AddVehicleViewModel
	{
		[Required]
		public Guid MakeId { get; set; } = new Guid();
		public List<AddVehicleMakeViewModel> Makes { get; set; } = new List<AddVehicleMakeViewModel>();

		[Required]
		[MinLength(ColorMinLength, ErrorMessage = ColorIsRequired)]
		[MaxLength(ColorMaxLength)]
		public string Color { get; set; } = null!;

		[Required]
		[MinLength(ModelMinLength)]
		[MaxLength(ModelMaxLength)]
		public string Model { get; set; } = null!;

		[Required]
		public Guid VehicleTypeId { get; set; }
		public List<AddVehicleVehicleTypeViewModel> VehicleTypes { get; set; } = new List<AddVehicleVehicleTypeViewModel>();

		[Required]
		public Guid TransmissionId { get; set; }
		public List<AddVehicleTransmissionViewModel> Transmissions { get; set; } = new List<AddVehicleTransmissionViewModel>();

		[Required]
		[Range(SeatsMinCount, SeatsMaxCount)]
		public int SeatsCount { get; set; }

		[Required]
		[Range(DoorsMinCount, DoorsMaxCount)]
		public int DoorsCount { get; set; }

		[Required]
		public string Year { get; set; } = null!;

		[Required]
		[Range(MileageMinNumber, MileageMaxNumber)]
		public int Mileage { get; set; }

		[MinLength(RegistrationMinLength)]
		[MaxLength(RegistrationMaxLength)]
		public string? RegistrationNumber { get; set; }

		[MinLength(ImageUrlMinLength)]
		[MaxLength(ImageUrlMaxLength)]
		public string? ImageUrl { get; set; }

		[MinLength(VINNumberMinLength)]
		[MaxLength(VINNumberMaxLength)]
		public string? VINNumber { get; set; }

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
