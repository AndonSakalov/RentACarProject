using RentACar.Web.ViewModels;
using RentACar.Web.ViewModels.Parts;

namespace RentACar.Services.Data.Interfaces
{
	public interface IMakeService
	{
		Task<IEnumerable<AddVehicleMakeViewModel>> GetAllMakesAsync();

		Task<bool> CreateMakeAsync(CreateMakeViewModel model);
	}
}
