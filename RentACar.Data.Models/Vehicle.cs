using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RentACar.Common.EntityValidationConstants.Vehicle;

namespace RentACar.Data.Models
{
    public class Vehicle
    {
        public Vehicle()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        [Comment("Identifier of the vehicle.")]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(Make))]
        [Comment("The Id of the vehicle make.")]
        public Guid MakeId { get; set; }
        public virtual Make Make { get; set; } = null!;

        [Required]
        [MaxLength(ColorMaxLength)]
        [Comment("Color of the car.")]
        public string Color { get; set; } = null!;

        [Required]
        [MaxLength(ModelMaxLength)]
        [Comment("The model of the car.")]
        public string Model { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(VehicleType))]
        [Comment("The Id of the type of the vehicle(SUV,Coupe,HatchBack...).")]
        public Guid VehicleTypeId { get; set; }
        public virtual VehicleType VehicleType { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Transmission))]
        [Comment("The Id of the transmission of the car.")]
        public Guid TransmissionId { get; set; }
        public virtual Transmission Transmission { get; set; } = null!;

        [Required]
        [Range(SeatsMinCount, SeatsMaxCount)]
        [Comment("The number of seats of the car.")]
        public int SeatsCount { get; set; }

        [Required]
        [Range(DoorsMinCount, DoorsMaxCount)]
        [Comment("The number of doors of the car.")]
        public int DoorsCount { get; set; }

        [Required]
        [Comment("Year of manufacturing.")]
        public DateTime Year { get; set; }

        [Required]
        [Range(MileageMinNumber, MileageMaxNumber)]
        [Comment("Mileage of the car.")]
        public int Mileage { get; set; }

        [MaxLength(RegistrationMaxLength)]
        [Comment("The registration number of the car.")]
        public string? RegistrationNumber { get; set; }

        [MaxLength(ImageUrlMaxLength)]
        [Comment("Url of the image of the car.")]
        public string? ImageUrl { get; set; }

        [MaxLength(VINNumberMaxLength)]
        [Comment("The VIN number of the car.")]
        public string? VINNumber { get; set; }

        [Required]
        [Comment("The date of addition of the car.")]
        public DateTime AddedOn { get; set; }

        [Required]
        [ForeignKey(nameof(Branch))]
        [Comment("The place from which the customer can take the car.")]
        public Guid BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Engine))]
        [Comment("The Id of the engine.")]
        public Guid EngineId { get; set; }
        public virtual Engine Engine { get; set; } = null!;

        [Required]
        [Range(PricePerDayMinValue, PricePerDayMaxValue)]
        [Precision(18, 2)]
        [Comment("The price of the vehicle per day.")]
        public decimal PricePerDay { get; set; }

        [ForeignKey(nameof(Rental))]
        [Comment("Id of the current rental for this car.")]
        public Guid? RentalId { get; set; }
        public virtual Rental? Rental { get; set; }
        public bool IsDeleted { get; set; }

        public bool IsRented { get; set; }
    }
}
