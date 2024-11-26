using System.ComponentModel.DataAnnotations;
using static RentACar.Common.EntityValidationConstants.Branch;
using static RentACar.Common.EntityValidationErrorMessages.Branch;

namespace RentACar.Web.ViewModels
{
    public class StaffSearchViewModel
    {
        [Required(ErrorMessage = CityRequiredMessage)]
        [MinLength(CityMinLength, ErrorMessage = CityMinLengthMessage)]
        [MaxLength(CityMaxLength, ErrorMessage = CityMaxLengthMessage)]
        public string City { get; set; } = null!;

        [Required]
        public Guid BranchId { get; set; }

        public List<StaffSearchBranchViewModel> FilteredBranches { get; set; } = new List<StaffSearchBranchViewModel>();
    }
}
