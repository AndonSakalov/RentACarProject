using RentACar.Web.ViewModels.Rental;

namespace RentACar.Services.Data.Interfaces
{
	public interface IRentalService
	{
		public Task<(bool isValid, DealViewModel? model)> ValidateInput(string branchId, string vehicleId, string pickupDate, string returnDate, string vehicleType, decimal price);

		public Task<bool> ReserveVehicle(DealViewModel model, string userId);
	}
}
