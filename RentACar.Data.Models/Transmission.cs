using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using static RentACar.Common.EntityValidationConstants.Transmission;

namespace RentACar.Data.Models
{
    public class Transmission
    {
        public Transmission()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        [Comment("Identifier of the transmission.")]
        public Guid Id { get; set; }

        [Required]
        [Range(MinGearsCount, MaxGearsCount)]
        [Comment("The count of gears for the transmission.")]
        public int GearsCount { get; set; }

        [Required]
        [Comment("The type of the transmission(manual or automatic).")]
        public TransmissionType Type { get; set; }
    }
}