using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels
{
    public class VehicleTypeViewModel
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Required]
        public string ImageUrl { get; set; } = null!;
    }
}
