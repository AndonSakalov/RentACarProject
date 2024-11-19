using RentACar.Web.ViewModels;

namespace RentACar.Services.Data.Interfaces
{
	public interface IBranchService
	{
		Task<IEnumerable<AddVehicleBranchViewModel>> GetAllBranchesAsync();
		Task<IEnumerable<BranchViewModel>> GetAllOrderedByLocationAsync(SearchBranchViewModel model);

		Task<IEnumerable<VehicleTypeViewModel>> GetAllVehicleTypesAsync(string id, string pickupDate, string returnDate);

		Task<IEnumerable<VehicleListViewModel>> GetAllVehiclesForCurrentBranchAsync(string id, string pickupDate, string returnDate, string vehicleTypeName);

		Task<IEnumerable<VehicleListViewModel>> GetAllVehiclesFilteredAsync(VehicleFilterViewModel model);
	}
}
