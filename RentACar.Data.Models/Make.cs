using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static RentACar.Common.EntityValidationConstants.Make;

namespace RentACar.Data.Models
{
    public class Make
    {
        public Make()
        {
            Id = Guid.NewGuid();
            Vehicles = new List<Vehicle>();
        }

        [Key]
        [Comment("Identifier of the make.")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("Name of the make.")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(CountryMaxLength)]
        [Comment("Country of the make.")]
        public string Country { get; set; } = null!;

        public bool IsDeleted { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}