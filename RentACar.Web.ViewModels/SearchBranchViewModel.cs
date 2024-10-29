using System.ComponentModel.DataAnnotations;
using static RentACar.Common.EntityValidationConstants.Branch;
using static RentACar.Common.EntityValidationErrorMessages.Branch;

namespace RentACar.Web.ViewModels
{
	public class SearchBranchViewModel
	{
		[Required(ErrorMessage = CityRequiredMessage)]
		[MinLength(CityMinLength, ErrorMessage = CityMinLengthMessage)]
		[MaxLength(CityMaxLength, ErrorMessage = CityMaxLengthMessage)]
		public string City { get; set; } = null!;

		[Required]
		public string PickUpDate { get; set; } = null!;

		[Required]
		public string ReturnDate { get; set; } = null!;
	}
}
