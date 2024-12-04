namespace RentACar.Web.ViewModels.Rental
{
	public class ReservationCompositeViewModel
	{
		public Dictionary<DateTime, StaffReservationViewModel> Reservations { get; set; } = new Dictionary<DateTime, StaffReservationViewModel>();

		public List<DateTime> UniqueDates { get; set; } = new List<DateTime>();
	}
}
