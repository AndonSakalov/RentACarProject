using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels.Rental
{
	public class StaffReservationViewModel
	{
		[Required]
		public string BranchName { get; set; } = null!;

		[Required]
		public Guid BranchId { get; set; }

		[Required]
		public Guid VehicleId { get; set; }

		[Required]
		public string CustomerName { get; set; } = null!;

		[Required]
		public string CustomerEmail { get; set; } = null!;

		[Required]
		public Guid CustomerId { get; set; }

		[Required]
		public DateTime PickUpDate { get; set; }

		[Required]
		public DateTime ReturnDate { get; set; }

		[Required]
		public decimal Price { get; set; }
		[Required]
		public string VehicleName { get; set; } = null!;

		public string? PhoneNumber { get; set; }

		[Required]
		public Guid ReservationId { get; set; }
	}

}
