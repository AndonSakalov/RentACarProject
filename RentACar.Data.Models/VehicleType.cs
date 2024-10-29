using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static RentACar.Common.EntityValidationConstants.VehicleType;

namespace RentACar.Data.Models
{
    public class VehicleType
    {
        public VehicleType()
        {
            Id = Guid.NewGuid();
            Vehicles = new List<Vehicle>();
        }

        [Key]
        [Comment("Identifier of the vehicle type.")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("Name of the vehicle type.")]
        public string Name { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        [Comment("Description of the vehicle type.")]
        public string? Description { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }

    }
}
