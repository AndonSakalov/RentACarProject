using RentACar.Web.ViewModels;
using RentACar.Web.ViewModels.Parts;

namespace RentACar.Services.Data.Interfaces
{
	public interface IEngineService
	{
		Task<IEnumerable<AddVehicleEngineViewModel>> GetAllEnginesAsync();

		Task<bool> CreateAndAddEngineAsync(CreateEngineViewModel model);
	}
}
