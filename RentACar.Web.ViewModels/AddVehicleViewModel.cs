using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models;
using System.ComponentModel.DataAnnotations;
using static RentACar.Common.EntityValidationConstants.Vehicle;
namespace RentACar.Web.ViewModels
{
    public class AddVehicleViewModel
    {
        [Required]
        public Guid MakeId { get; set; }
        public List<Make> Makes { get; set; } = new List<Make>();

        [Required]
        [MinLength(ColorMinLength)]
        [MaxLength(ColorMaxLength)]
        public string Color { get; set; } = null!;

        [Required]
        [MinLength(ModelMinLength)]
        [MaxLength(ModelMaxLength)]
        public string Model { get; set; } = null!;

        [Required]
        public Guid VehicleId { get; set; }
        public List<VehicleType> VehicleTypes { get; set; } = new List<VehicleType>();

        [Required]
        public Guid TransmissionId { get; set; }
        public List<Transmission> Transmissions { get; set; } = new List<Transmission>();

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
        public List<Branch> Branches { get; set; } = new List<Branch>();

        [Required]
        public Guid EngineId { get; set; }
        public List<Engine> Engines { get; set; } = new List<Engine>();

        [Required]
        [Range(PricePerDayMinValue, PricePerDayMaxValue)]
        [Precision(18, 2)]
        public decimal PricePerDay { get; set; }
    }
}
