using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels
{
    public class BranchViewModel
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public string Country { get; set; } = null!;

        [Required]
        public int VehiclesCount { get; set; }
    }
}
