using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels.Rental
{
	public class ConfirmReservationViewModel
	{
		[Required]
		public Guid CustomerId { get; set; }

		[Required]
		public DateTime PickupDate { get; set; }

		[Required]
		public DateTime ReturnDate { get; set; }

		[Required]
		public decimal Price { get; set; }

		[Required]
		public Guid VehicleId { get; set; }

		[Required]
		public Guid ReservationId { get; set; }

		[Required]
		public Guid BranchId { get; set; }
	}
}
