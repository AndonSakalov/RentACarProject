using System.ComponentModel.DataAnnotations;

namespace RentACar.Web.ViewModels.Rental
{
	public class RentalViewModel
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		public CustomerViewModel Customer { get; set; } = null!;

		[Required]
		public RentalVehicleViewModel Vehicle { get; set; } = new RentalVehicleViewModel();

		[Required]
		public Guid BranchId { get; set; }

		[Required]
		public string BranchName { get; set; } = null!;
		[Required]
		public DateTime StartDate { get; set; }
		[Required]
		public DateTime EndDate { get; set; }
		[Required]
		public decimal TotalPrice { get; set; }
		[Required]
		public PaymentViewModel Payment { get; set; } = null!;


	}
}
