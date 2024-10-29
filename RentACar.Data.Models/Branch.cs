using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static RentACar.Common.EntityValidationConstants.Branch;

namespace RentACar.Data.Models
{
    public class Branch
    {
        public Branch()
        {
            Id = Guid.NewGuid();
            Vehicles = new List<Vehicle>();
        }

        [Key]
        [Comment("Identifier of the branch.")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("The name of the branch.")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(AddressMaxLength)]
        [Comment("The address of the branch.")]
        public string Address { get; set; } = null!;

        [Required]
        [MaxLength(CityMaxLength)]
        [Comment("The city of the branch.")]
        public string City { get; set; } = null!;

        [Required]
        [MaxLength(CountryMaxLength)]
        [Comment("The country of the branch.")]
        public string Country { get; set; } = null!;
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
