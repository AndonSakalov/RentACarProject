using RentACar.Web.ViewModels;
using RentACar.Web.ViewModels.Parts;

namespace RentACar.Services.Data.Interfaces
{
    public interface ITransmissionService
    {
        Task<IEnumerable<AddVehicleTransmissionViewModel>> GetAllTransmissionsAsync();
        Task<bool> CreateTransmissionAsync(CreateTransmissionViewModel model);
        Task<IEnumerable<DeleteTransmissionViewModel>> GetAllTransmissionsForDeletionAsync();
        Task<bool> DeleteTransmissionAsync(Guid id);
    }
}
