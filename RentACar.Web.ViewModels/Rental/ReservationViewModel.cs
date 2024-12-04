using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels.Rental
{
    public class ReservationViewModel
    {
        [Required]
        public Guid BranchId { get; set; }

        [Required]
        public Guid VehicleId { get; set; }

        [Required]
        public DateTime PickUpDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public string VehicleName { get; set; } = null!;
    }
}
