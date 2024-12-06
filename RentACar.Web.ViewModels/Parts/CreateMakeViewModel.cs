using System.ComponentModel.DataAnnotations;
using static RentACar.Common.EntityValidationConstants.Make;
using static RentACar.Common.EntityValidationErrorMessages.Make;

namespace RentACar.Web.ViewModels.Parts
{
    public class CreateMakeViewModel
    {
        [Required(ErrorMessage = NameIsRequiredMessage)]
        [MinLength(NameMinLength, ErrorMessage = NameMinLengthMessage)]
        [MaxLength(NameMaxLength, ErrorMessage = NameMaxLengthMessage)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = CountryIsRequiredMessage)]
        [MinLength(CountryMinLength, ErrorMessage = CountryMinLengthMessage)]
        [MaxLength(CountryMaxLength, ErrorMessage = CountryMaxLengthMessage)]
        public string Country { get; set; } = null!;
    }
}
