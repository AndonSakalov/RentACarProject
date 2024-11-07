using RentACar.Web.ViewModels;

namespace RentACar.Services.Data.Interfaces
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchViewModel>> GetAllOrderedByLocationAsync(SearchBranchViewModel model);

        Task<IEnumerable<VehicleTypeViewModel>> GetAllVehicleTypesAsync(string id);
    }
}
