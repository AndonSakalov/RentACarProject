using RentACar.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using static RentACar.Common.EntityValidationConstants.Transmission;
using static RentACar.Common.EntityValidationErrorMessages.Transmission;

namespace RentACar.Web.ViewModels.Parts
{
	public class CreateTransmissionViewModel
	{
		[Required(ErrorMessage = GearsCountIsRequired)]
		[Range(MinGearsCount, MaxGearsCount, ErrorMessage = GearsCountRangeMessage)]
		public int GearsCount { get; set; }

		[Required]
		public TransmissionType TransmissionType { get; set; }

		public string DisplayName => $"{TransmissionType.ToString()} transmission with {GearsCount} gears.";
	}
}
