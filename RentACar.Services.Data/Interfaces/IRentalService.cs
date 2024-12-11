using RentACar.Web.ViewModels.Rental;

namespace RentACar.Services.Data.Interfaces
{
	public interface IRentalService
	{
		public Task<(bool isValid, DealViewModel? model)> ValidateInputAsync(string branchId, string vehicleId, string pickupDate, string returnDate, string vehicleType, decimal price);
		public Task<bool> ReserveVehicleAsync(DealViewModel model, string userId);
		public Task<(bool isIdValid, ReservationCompositeViewModel? dict)> GetReservationsAsync(string branchId);
		public Task<(bool isSuccessful, bool isVehicleFree)> SetReservationAsRental(ConfirmReservationViewModel model);
		public Task<(bool isSuccessful, RentalsCompositeViewModel? rentals)> GetAllRentalsForBranchAsync(string branchId);
		public Task<(bool isSuccessful, EndRentalViewModel? model)> GetVehicleRentalToRemoveAsync(string rentalId, string vehicleId);
		public Task<bool> EndRentalAsync(EndRentalViewModel model);
	}
}
