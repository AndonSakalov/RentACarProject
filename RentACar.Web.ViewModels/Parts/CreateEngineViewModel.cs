using RentACar.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using static RentACar.Common.EntityValidationConstants.Engine;
using static RentACar.Common.EntityValidationErrorMessages.Engine;

namespace RentACar.Web.ViewModels.Parts
{
    public class CreateEngineViewModel
    {
        [Required]
        public FuelType FuelType { get; set; }

        [Required(ErrorMessage = HPIsRequiredMessage)]
        [Range(HPMinRange, HPMaxRange, ErrorMessage = HPRangeMessage)]
        public int HP { get; set; }

        [Range(MinTorque, MaxTorque, ErrorMessage = TorqueRangeMessage)]
        public int? Torque { get; set; }

        [Required(ErrorMessage = FuelEfficiencyIsRequiredMessage)]
        [Range(FuelEfficiencyMinForElectric, FuelEfficiencyMax, ErrorMessage = FuelEfficiencyRangeMessage)]
        public decimal FuelEfficiency { get; set; }

        [Required(ErrorMessage = DisplacementIsRequiredMessage)]
        [Range(DisplacementMinRangeForElectric, DisplacementMaxRange, ErrorMessage = DisplacementRangeMessage)]
        public decimal Displacement { get; set; }

        [Required(ErrorMessage = CylindersCountIsRequiredMessage)]
        [Range(CylindersMinCountForElectric, CylindersMaxCount, ErrorMessage = CylindersCountRangeMessage)]
        public int CylindersCount { get; set; }

        [MaxLength(DescriptionMaxLength, ErrorMessage = DescriptionMaxLengthMessage)]
        public string? Description { get; set; }

        [Required]
        public bool IsElectric => FuelType.ToString() == "Electric" ? true : false;
    }
}
