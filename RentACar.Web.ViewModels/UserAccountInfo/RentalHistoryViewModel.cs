using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels.UserAccountInfo
{
    public class RentalHistoryViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string VehicleName { get; set; } = null!;

        [Required]
        public Guid VehicleId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal RentalPrice { get; set; }
    }
}
