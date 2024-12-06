using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using static RentACar.Common.EntityValidationConstants.Engine;

namespace RentACar.Data.Models
{
    public class Engine
    {
        public Engine()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [Comment("The type of fuel that the engine uses.")]
        public FuelType FuelType { get; set; }

        [Required]
        [Range(HPMinRange, HPMaxRange)]
        [Comment("The horse power of the engine.")]
        public int HP { get; set; }

        [Range(MinTorque, MaxTorque)]
        [Comment("The torque of the car in Nm.")]
        public int? Torque { get; set; }

        [Required]
        [Precision(18, 2)]
        [Range(FuelEfficiencyMin, FuelEfficiencyMax)]
        [Comment("How much the engine burns per 100km.")]
        public decimal FuelEfficiency { get; set; }

        [Required]
        [Precision(18, 2)]
        [Range(DisplacementMinRange, DisplacementMaxRange)]
        [Comment("The measurement of the total volume of all of an engine's cylinders.")]
        public decimal Displacement { get; set; }

        [Required]
        [Range(CylindersMinCount, CylindersMaxCount)]
        [Comment("The cylinders count of the engine.")]
        public int CylindersCount { get; set; }

        [Comment("Optional description of the engine.")]
        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [Comment("Whether the engine is electric or not.")]
        public bool IsElectric { get; set; }

        public bool IsDeleted { get; set; }
    }
}
