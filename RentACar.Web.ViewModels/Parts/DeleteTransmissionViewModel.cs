using System.ComponentModel.DataAnnotations;
using static RentACar.Common.EntityValidationConstants.Transmission;
using static RentACar.Common.EntityValidationErrorMessages.Transmission;
namespace RentACar.Web.ViewModels.Parts
{
	public class DeleteTransmissionViewModel
	{
		[Required]
		public Guid Id { get; set; }

		[Required(ErrorMessage = GearsCountIsRequired)]
		[Range(MinGearsCount, MaxGearsCount, ErrorMessage = GearsCountRangeMessage)]
		public int GearsCount { get; set; }

		[Required]
		public string TransmissionType { get; set; } = null!;

		public string DisplayName => $"{TransmissionType.ToString()} transmission with {GearsCount} gears.";
	}
}
