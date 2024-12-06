using RentACar.Web.ViewModels;
using RentACar.Web.ViewModels.Parts;

namespace RentACar.Services.Data.Interfaces
{
    public interface IEngineService
    {
        Task<IEnumerable<AddVehicleEngineViewModel>> GetAllEnginesAsync();
        Task<IEnumerable<DeleteEngineViewModel>> GetAllEnginesForDeletionAsync();
        Task<bool> CreateAndAddEngineAsync(CreateEngineViewModel model);
        Task<bool> DeleteEngineAsync(Guid engineId);

    }
}
