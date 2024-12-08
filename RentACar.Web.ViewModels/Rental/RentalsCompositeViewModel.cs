namespace RentACar.Web.ViewModels.Rental
{
	public class RentalsCompositeViewModel
	{
		public List<RentalViewModel> Rentals { get; set; } = new List<RentalViewModel>();

		public List<DateTime> UniqueDates { get; set; } = new List<DateTime>();
	}
}
