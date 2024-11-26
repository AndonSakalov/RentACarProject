using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels
{
    public class StaffSearchBranchViewModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public int VehiclesCount { get; set; }
    }
}
