using RentACar.Web.ViewModels;

namespace RentACar.Services.Data.Interfaces
{
	public interface IBranchService
	{
		Task<IEnumerable<BranchViewModel>> GetAllOrderedByLocationAsync(SearchBranchViewModel model);

		Task<IEnumerable<VehicleTypeViewModel>> GetAllVehicleTypesAsync(string id, string pickupDate, string returnDate);

		Task<IEnumerable<VehicleListViewModel>> GetAllVehiclesForCurrentBranch(string id, string pickupDate, string returnDate, string vehicleTypeName);

		Task<IEnumerable<VehicleListViewModel>> GetAllVehiclesFiltered(VehicleFilterViewModel model);
	}
}
